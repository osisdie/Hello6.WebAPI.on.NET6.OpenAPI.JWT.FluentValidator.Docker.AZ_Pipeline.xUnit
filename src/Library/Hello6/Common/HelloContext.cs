using CoreFX.Common.App_Start;
using Hello6.Domain.Common.Models;

namespace Hello6.Domain.Common
{
    public sealed class HelloContext : SvcContext
    {
        public static HelloConfiguration Settings = new HelloConfiguration();
    }
}
