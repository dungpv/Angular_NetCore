using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeSpace.BackendServer.Helper
{
    public class ApiNotFoundResponse : ApiResponse
    {
        public ApiNotFoundResponse(string message)
             : base(404, message)
        {
        }
    }
}
