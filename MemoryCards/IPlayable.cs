using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCards
{
    interface IPlayable
    {
        void HideCard(int id);
        void ShowCard(int id, int image);
        void Win();

    }
}
