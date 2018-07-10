using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetter.Classes.AppData
{
    class SavedData
    {
        public static Type GetStartingPage() => typeof(Pages.Subnetting.NewNetPage);
    }
}
