using Structs;
using VDFramework;

namespace Gameplay
{
    public class House : BetterMonoBehaviour
    {
        private HouseData data;
    
        public void Instantiate(HouseData houseData)
        {
            data = houseData;
        }
    }
}
