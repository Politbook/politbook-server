using EleicaoBrasil.Model.DataExceptions;
using System;
using EleicaoBrasil.Model.Interfaces;
using EleicaoBrasil.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleicaoBrasil.Model.Interfaces
{
    public class UserRepository : IDisposable, IUserRepository
    {
        private EleicaoContext db = new EleicaoContext();

        public User Add(User user)
        {
            try
            {
                User user1 = db.Users.Where(p => p.idSocial.Equals(user.idSocial)).FirstOrDefault();

                if (user1 == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    return user;
                }
                else
                {
                    user1.email = user.email;
                    user1.name = user.name;
                    user1.lastName = user.lastName;
                    user1.firstName = user.firstName;
                    user1.gender = user.gender;
                    user1.idSocial = user.idSocial;
                    user1.photo = user.photo;
                    user1.birthday = user.birthday;

                    db.Entry(user1).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return user1;
            }
            catch (Exception ex)
            {
                throw new DataException("Erro ao inserir usuario." + ex.Message);
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
