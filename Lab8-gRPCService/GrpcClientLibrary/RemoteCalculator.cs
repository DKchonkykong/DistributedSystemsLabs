using Grpc.Net.Client;
using System.Threading.Tasks;

namespace GrpcClientLibrary
{
    // remote calculaor library this can be used by the client 
    public class RemoteCalculator
    {
        private readonly GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7123");
        private readonly Calculator.CalculatorClient client;

        public RemoteCalculator()
        {
            client = new Calculator.CalculatorClient(channel);
        }
   
        public async Task<int> Add(int valueA, int valueB)
        {
            TwoIntsRequests request = new TwoIntsRequests
            {
                A = valueA,
                B = valueB
            };
            IntValueReply response = await client.AddAsync(request);
            return response.Value;
        }

        public async Task<int> Multiply(int valueA, int valueB)
        {
            TwoIntsRequests request = new TwoIntsRequests
            {
                A = valueA,
                B = valueB
            };
            IntValueReply response = await client.MultiplyAsync(request);
            return response.Value;
        }

        public async Task<float> Divide(int valueA, int valueB)
        {
            TwoIntsRequests request = new TwoIntsRequests
            {
                A = valueA,
                B = valueB
            };
            FloatValueReply response = await client.DivideAsync(request);
            return response.Value;
        }
    }
}
