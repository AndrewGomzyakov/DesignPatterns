using Adapter.FirstOrmLibrary;
using Adapter.Models;
using Example_04.Homework.SecondOrmLibrary;

namespace Adapter.Clients
{
    public class UserClient
    {
        private IOrmAdapter _ormAdapter;

        private IFirstOrm<DbUserEntity> _firstOrm1;
        private IFirstOrm<DbUserInfoEntity> _firstOrm2;

        private ISecondOrm _secondOrm;

        private bool _useFirstOrm = true;

        public (DbUserEntity, DbUserInfoEntity) Get(int userId)
        {
            return _ormAdapter.Get(userId);
        }

        public void Add(DbUserEntity user, DbUserInfoEntity userInfo)
        {
            _ormAdapter.Add(user, userInfo);
        }

        public void Remove(int userId)
        {
            _ormAdapter.Remove(userId);
        }

        public UserClient(IOrmAdapter ormAdapter)
        {
            _ormAdapter = ormAdapter;
        }
    }
}
