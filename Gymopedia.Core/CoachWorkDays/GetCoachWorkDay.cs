using Gymopedia.Core.Models;
using Gymopedia.Domain.Repositories;
using Gymopedia.Domain.Models;
using Gymopedia.Infrastructure.Constants;
using MediatR;

namespace Gymopedia.Core.CoachWorkDays
{
    internal class GetCoachWorkDay
    {
        public record Request(int ID) : IRequest<Response>;
        public record Response(CoachWorkDay? СoachWorkDay, string? Error = null);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ICoachWorkDayRepository _сoachWorkDayRepository;
            public Handler(ICoachWorkDayRepository сoachWorkDayRepository)
            {
                _сoachWorkDayRepository = сoachWorkDayRepository;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var сoachWorkDay = await _сoachWorkDayRepository.Get(request.ID, cancellationToken);
                if (сoachWorkDay == null)
                {
                    return new Response(null, Constants.ErrorMessage.CoachWorkDay.CoachWorkDayNotFoundError);
                }
                return new Response(сoachWorkDay);
            }
        }
    }
}
