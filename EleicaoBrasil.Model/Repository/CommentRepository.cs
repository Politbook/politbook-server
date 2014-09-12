using EleicaoBrasil.Model.Interfaces;
using EleicaoBrasil.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleicaoBrasil.Model.DataExceptions;

namespace EleicaoBrasil.Model.Repository
{
    public class CommentRepository : IDisposable, ICommentRepository
    {
        private EleicaoContext db = new EleicaoContext();

        public object GetCommentsCandidate(string idCandidate, int _page = 1, int _offset = 10)
        {
            try
            {
                var comments = from p in db.Comments
                               where p.idCandidate.Equals(idCandidate)
                               orderby p.date descending
                               select new { day = p.date.Day, month = p.date.Month, year = p.date.Year, comment = p.comment, rating = p.rating, nameUser = p.user.name, idSocial = p.user.idSocial };
                return comments.ToList().Skip((_page - 1) * _offset).Take(_offset);

            }
            catch (Exception ex)
            {
                throw new DataException("Erro ao consultar comentarios." + ex.Message );
            }
        }

        public IEnumerable<Comment> GetLastCommentsCandidate(string idCandidate, int qtd = 10)
        {
            try
            {
                return db.Comments.Where(p => p.idCandidate.Equals(idCandidate)).Take(qtd);
            }
            catch (Exception ex)
            {
                throw new DataException("Erro ao consultar ultimos comentarios." + ex.Message);
            }
        }

        public object GetComment(int idUser, string idCandidate)
        {
            try
            {

                var comments = from p in db.Comments
                               where p.idCandidate.Equals(idCandidate) && p.idUser == idUser
                               orderby p.date descending
                               select new { day = p.date.Day, month = p.date.Month, year = p.date.Year, comment = p.comment, rating = p.rating, nameUser = p.user.name, idSocial = p.user.idSocial };

                return comments.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DataException("Erro ao consultar comentario." + ex.Message);
            }
        }

        public Candidate GetCandidateRating(string idCandidate)
        {
            try
            {
                var candidate = ( from p in db.Candidates
                               where p.id.Equals(idCandidate)
                               select new { rating = p.average, qtd = p.qtd }).FirstOrDefault();

                if (candidate == null)
                    return null;

                return new Candidate() { qtd = candidate.qtd, average = candidate.rating };
            }
            catch (Exception ex)
            {
                throw new DataException("Erro ao consultar candidato." + ex.Message);
            }
        }

        public void Add(Comment comment)
        {

            if (comment == null)
            {
                throw new DataException("Commentario nao informado.");
            }

            try
            {
                var commentUser = db.Comments.Where(p => p.idUser == comment.idUser && p.idCandidate == comment.idCandidate).FirstOrDefault();

                if (comment.candidate != null)
                {
                    var candidate = db.Candidates.Where(p => p.id.Equals(comment.idCandidate)).FirstOrDefault();
                    if (candidate == null)
                    {
                        comment.candidate.id = comment.idCandidate;
                        comment.candidate.qtd += 1;
                        comment.candidate.total += comment.rating;
                        comment.candidate.average = (float)comment.candidate.total / (float) comment.candidate.qtd;
                        db.Candidates.Add(comment.candidate);
                    }
                    else
                    {
                        if (commentUser != null)
                        {
                            candidate.total -= commentUser.rating;
                        }
                        else
                        {
                            candidate.qtd += 1;
                        }

                        candidate.total += comment.rating;
                        candidate.average = (float) candidate.total / (float) candidate.qtd;
                   
                        db.Entry(candidate).State = EntityState.Modified;
                    }

                    comment.candidate = null;
                }

                
                if (commentUser == null)
                {
                    comment.date = DateTime.Now;
                    db.Comments.Add(comment);
                }
                else
                {
                    commentUser.rating = comment.rating;
                    commentUser.comment = comment.comment;
                    commentUser.latitude = comment.latitude;
                    commentUser.longitude = comment.longitude;
                    comment.date = DateTime.Now;
                    db.Entry(commentUser).State = EntityState.Modified;
                }
               
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DataException("Erro ao inserir comentario." + ex.Message);
            }
        }

        public void Update(Comment comment)
        {
            try
            {
                db.Entry(comment).State = EntityState.Modified;
                comment.date = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DataException("Erro ao atualizar comentario." + ex.Message);
            }
            
        }

        public void Delete(string idCandidate, int idUser)
        {
            try
            {
                Comment comment = db.Comments.Where(p => p.idCandidate == idCandidate && p.idUser == idUser).FirstOrDefault();
                if (comment != null)
                {
                    var candidate = db.Candidates.Where(p => p.id.Equals(comment.candidate.id)).FirstOrDefault();
                    if (candidate != null)
                    {
                        candidate.qtd -= 1;
                        candidate.total -= comment.rating;

                        if (candidate.qtd <= 0)
                        {
                            candidate.qtd = 0;
                            candidate.average = 0;
                        }
                        else
                        {
                            candidate.average = (float)candidate.total / (float)candidate.qtd;
                        }
                        
                        db.Entry(candidate).State = EntityState.Modified;
                    }

                    db.Comments.Remove(comment);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Erro ao deletar comentario." + ex.Message);
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
