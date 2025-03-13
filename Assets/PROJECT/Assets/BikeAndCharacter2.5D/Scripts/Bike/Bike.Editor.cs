#if UNITY_EDITOR
namespace Kamgam.BikeAndCharacter25D
{
    public partial class Bike // .Editor
    {
        public void OnValidate()
        {
            BackWheelGroundTouchTrigger.GroundLayers = GroundLayers;
            BackWheelOuterGroundTouchTrigger.GroundLayers = GroundLayers;
            FrontWheelGroundTouchTrigger.GroundLayers = GroundLayers;
        }
    }
}
#endif
