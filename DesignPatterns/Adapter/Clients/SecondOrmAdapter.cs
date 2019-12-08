using System.Linq;
using Adapter.Models;
using Example_04.Homework.SecondOrmLibrary;

namespace Adapter.Clients
{
    public class SecondOrmAdapter : IOrmAdapter
    {
        private readonly ISecondOrm _secondOrm;

        public SecondOrmAdapter(ISecondOrm secondOrm)
        {
            _secondOrm = secondOrm;
        }
        
        public (DbUserEntity, DbUserInfoEntity) Get(int userId)
        {
            var user = _secondOrm.Context.Users.First(i => i.Id == userId);
            var userInfo = _secondOrm.Context.UserInfos.First(i => i.Id == user.InfoId);
            return (user, userInfo);
        }

        public void Add(DbUserEntity user, DbUserInfoEntity userInfo)
        {
            _secondOrm.Context.Users.Add(user);
            _secondOrm.Context.UserInfos.Add(userInfo);
        }

        public void Remove(int userId)
        {
            var user = _secondOrm.Context.Users.FirstOrDefault(u => u.Id == userId);
            var userInfo = _secondOrm.Context.UserInfos.FirstOrDefault(u => u.Id == userId);
            _secondOrm.Context.Users.Remove(user);
            _secondOrm.Context.UserInfos.Remove(userInfo);
        }
    }
}