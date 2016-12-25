namespace Synapse.Databases
{
    using log4net;
    using log4net.Config;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TransactionDBManager
    {

        private TransactionsEntities _context;
        private static TransactionDBManager _manager;
        private static readonly ILog log = LogManager.GetLogger(typeof(TransactionDBManager));

        /// <summary>
        /// Конструктор.
        /// </summary>
        public TransactionDBManager()
        {
            XmlConfigurator.Configure();
            AutoSave = true;
            _context = new TransactionsEntities();
            _context.TStrategies.Load();
            ////dgvComputers.DataSource = _context.Computers.Local.ToBindingList();
            _manager = this;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="connectionName">Имя соединения.</param>
        public TransactionDBManager(string connectionName)
        {
            XmlConfigurator.Configure();
            AutoSave = true;
            ConnectionName = connectionName;
            //_context = new TransactionsEntities(connectionName);

            _context.TStrategies.Load();
            //dgvComputers.DataSource = _context.Computers.Local.ToBindingList();
            _manager = this;
        }

        #region Properties
        /// <summary>
        /// Флаг - вызывать сохраннение изменений в базе данных автоматически. 
        /// </summary>
        public bool AutoSave { set; get; }

        /// <summary>
        /// Имя соединения.
        /// </summary>
        public string ConnectionName { private set; get; }

        #endregion

        #region Methods

        /// <summary>
        /// Возвращает экземпляр менеджера базы данных.
        /// </summary>
        /// <returns>Экземпляр менеджера базы данных</returns>
        public static TransactionDBManager GetInstance()
        {
            return _manager;
        }

        /// <summary>
        /// Добавляет в базу данных информацию о стратегии.
        /// </summary>
        /// <param name="sInfo">Информация о стратегии.</param>
        /// <param name="psInfo">Информация о родительской стратегии.</param>
        public void AddStrategy(IStrategyInfo sInfo, IStrategyInfo psInfo)
        {

            try
            {
                TStrategy parent = null;

                if (_context.TStrategies.FirstOrDefault(s => s.GUID.CompareTo(sInfo.GUID) == 0) != null)
                    throw new Exception("Стратегия уже добавлена.");

                var strategy = sInfo.ToStrategy();

                if (psInfo != null)
                {
                    parent = _context.TStrategies.FirstOrDefault(s => s.GUID.CompareTo(sInfo.GUID) == 0);

                    if (parent == null)
                        throw new ArgumentNullException(nameof(parent));

                    strategy.ParentId = parent.StrategyId;
                    strategy.TStrategy1 = parent;

                }

                _context.TStrategies.Add(strategy);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Error("err.", ex);
            }

        }

        /// <summary>
        /// Удаляет из базы данных информацию о стратегии.
        /// </summary>
        /// <param name="sInfo">Информация о стратегии.</param>
        public void RemoveStrategy(IStrategyInfo sInfo)
        {
            try
            {
                var strategy = _context.TStrategies.FirstOrDefault(s => s.GUID.CompareTo(sInfo.GUID) == 0);

                if (strategy == null)
                    throw new ArgumentNullException(nameof(strategy));

                _context.TStrategies.Remove(strategy);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                log.Error("err.", ex);
            }
        }

        /// <summary>
        /// Добавляет в базу данных информацию о заявке.
        /// </summary>
        /// <param name="oInfo">Информация о заявке.</param>
        /// <param name="sInfo">Информация о стратегии, к которой отностися заявка.</param>
        public void AddOrder(IOrderInfo oInfo, IStrategyInfo sInfo)
        {
            var strategy = _context.TStrategies.FirstOrDefault(s => s.GUID.CompareTo(sInfo.GUID) == 0);

            if (strategy == null)
                throw new ArgumentNullException(nameof(strategy));

            try
            {
                if (_context.TOrders.Any(o => o.TransactionId == oInfo.TransactionId))
                    return;

                var order = oInfo.ToOrder();
                order.TStrategy = strategy;
                _context.TOrders.Add(order);

                if (AutoSave)
                    _context.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Error("err.", ex);
            }

        }

        /// <summary>
        /// Обновляет в базе данных информацию о заявке.
        /// </summary>
        /// <param name="oInfo">Информация о заявке.</param>
        /// <param name="sInfo">Информация о стратегии, к которой отностися заявка.</param>
        public bool UpdateOrder(IOrderInfo oInfo, IStrategyInfo sInfo)
        {
            var isUpdate = false;

            try
            {

                var order = _context.TOrders.FirstOrDefault(o => o.TransactionId == oInfo.TransactionId);

                if (order == null && sInfo != null)
                {
                    AddOrder(oInfo, sInfo);
                    order = _context.TOrders.FirstOrDefault(o => o.TransactionId == oInfo.TransactionId);
                }

                if (order == null)
                    throw new ArgumentNullException(nameof(order));

                isUpdate = order.Update(oInfo);

                if (AutoSave)
                    _context.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Error("err.", ex);
            }

            return isUpdate;

        }

        /// <summary>
        /// Удаляет из базы данных информацию о заявке.
        /// </summary>
        /// <param name="oInfo">Информация о заявке.</param>
        public void RemoveOrder(IOrderInfo oInfo)
        {
            try
            {
                var order = _context.TOrders.FirstOrDefault(o => o.TransactionId == oInfo.TransactionId);

                if (order == null)
                    throw new ArgumentNullException(nameof(order));

                _context.TOrders.Remove(order);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                log.Error("err.", ex);
            }

        }

        /// <summary>
        /// Добавляет в базу данных информацию о собственной сделке.
        /// </summary>
        /// <param name="tInfo">Информация о собственной сделке.</param>
        /// <param name="oInfo">Информация о заявке, к которой относится сделка.</param>
        public void AddTrade(ITradeInfo tInfo, IOrderInfo oInfo)
        {

            try
            {
                var order = _context.TOrders.FirstOrDefault(o => o.TransactionId == oInfo.TransactionId);

                if (order == null)
                    throw new ArgumentNullException(nameof(order));

                if (_context.TTrades.Any(o => o.ExchTradeId == tInfo.ExchTradeId && o.Time == tInfo.Time))
                    return;

                var trade = tInfo.ToTrade();

                _context.TTrades.Add(trade);

                if (AutoSave)
                    _context.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Error("err.", ex);
            }

        }

        /// <summary>
        /// Сохраняет изменения в базе данных.
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Сохраняет изменения в базе данных ассинхронно.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освободить ресурсы.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion

    }

}





