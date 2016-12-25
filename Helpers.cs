using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.Databases
{
    public static class Helpers
    {

        /// <summary>
        /// Конвертирует информацию о стратегии в стратегию.
        /// </summary>
        /// <param name="info">Информация о стратегии.</param>
        /// <returns>Стратегия.</returns>
        public static TStrategy ToStrategy(this IStrategyInfo info)
        {
            return new TStrategy
            {
                Name = info.Name,
                GUID = info.GUID
            };
        }

        /// <summary>
        /// Конвертирует информацию о заявке в заявку.
        /// </summary>
        /// <param name="info">Информация о заявке.</param>
        /// <returns>Заявка.</returns>
        public static TOrder ToOrder(this IOrderInfo info)
        {
            return new TOrder
            {
                TransactionId = info.TransactionId,
                ExchOrderId = info.ExchOrderId,
                Time = info.Time,
                ChangeTime = info.ChangeTime,
                Price = info.Price,
                Volume = info.Volume,
                Balance = info.Balance,
                State = info.State,
                Type = info.Type,
                Side = info.Side,
                Canceled = info.Canceled,
                Portfolio = info.Portfolio,
                Security = info.Security,
                CancelLatency = info.CancelLatency,
                RegisterLatency = info.RegisterLatency
            };
        }

        /// <summary>
        /// Обновляет информацию в заявке. Возвращает true, если изменился статус, баланс или индикатор отмены заявки.   
        /// </summary>
        /// <param name="order">Заявка.</param>
        /// <param name="info">Информация о заявке.</param>
        /// <returns>Была ли изменена информация в заявке.</returns>
        public static bool Update(this TOrder order, IOrderInfo info)
        {
            var isUpdated = false;

            order.ExchOrderId = info.ExchOrderId;
            order.ChangeTime = info.ChangeTime;

            if (order.Balance != info.Balance)
            {
                order.Balance = info.Balance;
                if (!isUpdated) isUpdated = true;
            }
                
            if (order.State != info.State)
            {
                order.State = info.State;
                if (!isUpdated) isUpdated = true;
            }

            if (order.Canceled != info.Canceled)
            {
                order.Canceled = info.Canceled;
                if (!isUpdated) isUpdated = true;
            }

            order.CancelLatency = info.CancelLatency;
            order.RegisterLatency = info.RegisterLatency;

            return isUpdated;
        }

        /// <summary>
        /// Конвертирует информацию о сделке в сделку.
        /// </summary>
        /// <param name="info">Информация о сделке.</param>
        /// <returns>Сделка.</returns>
        public static TTrade ToTrade(this ITradeInfo info)
        {
            return new TTrade
            {
                ExchTradeId = info.ExchTradeId,
                Time = info.Time,
                Price = info.Price,
                Volume = info.Volume,
                Side = info.Side
            };
        }

    }
}
