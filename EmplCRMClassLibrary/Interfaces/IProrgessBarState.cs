using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmplCRMClassLibrary.Interfaces
{
    public interface IProrgessBarState
    {
        event Action<String> UpdateStatusText;

        event Action<int> UpdatePercentage;

    }
}
