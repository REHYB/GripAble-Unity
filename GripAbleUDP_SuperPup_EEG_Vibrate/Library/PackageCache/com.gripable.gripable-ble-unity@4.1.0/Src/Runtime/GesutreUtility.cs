using Protos;

public class GestureUtility
{

	public static float GetTranslation(float min, float max, GestureType gestureType, float scaleFactor)
	{
        if (!(gestureType.Equals(GestureType.Squeeze) || gestureType.Equals(GestureType.Release)))
        {
		    max = Rotate180(max);
		    min = Rotate180(min);
        }
        float translationMagnitude = scaleFactor * (max - min);

        switch (gestureType)
        {
            case GestureType.Squeeze:
            case GestureType.Supination:
            case GestureType.Flexion:
            case GestureType.Ulnar:
                return translationMagnitude;
            case GestureType.Release:
            case GestureType.Pronation:
            case GestureType.Extension:
            case GestureType.Radial:
                return -translationMagnitude;
            default:
                return 0;
        }
	}

	private static float Rotate180(float angle) { return (angle + 180) % 360; }
}
