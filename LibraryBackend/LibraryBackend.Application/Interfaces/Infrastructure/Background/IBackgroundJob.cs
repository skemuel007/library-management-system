using System.Linq.Expressions;
using LibraryBackend.Core.Enums;

namespace LibraryBackend.Application.Interfaces.Infrastructure.Background;

public interface IBackgroundJob
{
    string AddEnque(Expression<Action> methodCall);

    string AddEnque<T>(Expression<Action<T>> methodCall);

    string AddContinuations(Expression<Action> methodCall, string jobid);

    string AddContinuations<T>(Expression<Action<T>> methodCall, string jobid);

    string AddSchedule(Expression<Action> methodCall, RecuringTime recuringTime, double time);

    string AddSchedule<T>(Expression<Action<T>> methodCall, RecuringTime recuringTime, double time);
}
