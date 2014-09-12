using System;
using System.Collections.Generic;

namespace EleicaoBrasil.Model.Interfaces
{
    public interface ICommentRepository
    {
        object GetCommentsCandidate(string idCandidate, int _page = 1, int _offset = 10);

        IEnumerable<Comment> GetLastCommentsCandidate(string idCandidate, int qtd = 10);

        object GetComment(int idUser, string idCandidate);

        Candidate GetCandidateRating(string idCandidate);

        void Add(Comment comment);

        void Update(Comment comment);

        void Delete(string idCandidate, int idUser);
    }
}
