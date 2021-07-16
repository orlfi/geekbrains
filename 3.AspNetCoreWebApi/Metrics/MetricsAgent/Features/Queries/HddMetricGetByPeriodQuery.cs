﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Core.Responses;
using MetricsAgent.DAL.Interfaces;

using AutoMapper;

namespace MetricsAgent.Features.Queries
{
    public class HddMetricGetByPeriodQuery : IRequest<AgentHddMetricResponse>
    {
        public DateTimeOffset FromTime { get; set; }

        public DateTimeOffset ToTime { get; set; }

        public override string ToString()
        {
            return $"FromTime={FromTime} ToTime={ToTime}";
        }

        public class HddMetricGetByPeriodQueryHandler : IRequestHandler<HddMetricGetByPeriodQuery, AgentHddMetricResponse>
        {
            private readonly IHddMetricsRepository _repository;
            private readonly IMapper _mapper;

            public HddMetricGetByPeriodQueryHandler(IHddMetricsRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<AgentHddMetricResponse> Handle(HddMetricGetByPeriodQuery request, CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    var metricsList = _repository.GetByPeriod(request.FromTime, request.ToTime);

                    var response = new AgentHddMetricResponse();

                    response.Metrics.AddRange(_mapper.Map<List<AgentHddMetricDto>>(metricsList));

                    return response;
                });

                return result;
            }
        }
    }
}
