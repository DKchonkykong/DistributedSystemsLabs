using Grpc.Core;
using Lab8_gRPCService;

namespace Lab8_gRPCService.Services
{
    public class CalculatorService : Calculator.CalculatorBase
    {
        private readonly ILogger<CalculatorService> _logger;
        public CalculatorService(ILogger<CalculatorService> logger)
        {
            _logger = logger;
        }

        public override Task<IntValueReply> Add(TwoIntsRequests request, ServerCallContext context)
        {
            return Task.FromResult(new IntValueReply
            {
                Value = request.A + request.B
            });
        }
        public override Task<IntValueReply> Multiply(TwoIntsRequests request, ServerCallContext context)
        {
            return Task.FromResult(new IntValueReply
            {
                Value = request.A * request.B
            });
        }


        public override Task<FloatValueReply> Divide(TwoIntsRequests request, ServerCallContext context)
        {
            if (request.B == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Denominator cannot be zero."));
            }

            return Task.FromResult(new FloatValueReply
            {
                Value = (float)request.A / (float)request.B
            });
        }


    }
}
