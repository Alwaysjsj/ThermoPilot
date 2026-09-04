using Core.Common;

namespace Core.Entities
{
    public class ExtControlEntity : PropertyChangedBase
    {
        private string? _ControlTarget;

        public string? ControlTarget
        {
            get
            {
                return _ControlTarget;
            }

            set
            {
                _ControlTarget = value;
                NotifyOfPropertyChange(nameof(ControlTarget));
            }
        }

        private float _SetValue;

        public float SetValue
        {
            get
            {
                return _SetValue;
            }

            set
            {
                _SetValue = value;
                NotifyOfPropertyChange(nameof(SetValue));
            }
        }

        private float _AlmMax;

        public float AlmMax
        {
            get
            {
                return _AlmMax;
            }

            set
            {
                _AlmMax = value;
                NotifyOfPropertyChange(nameof(AlmMax));
            }
        }

        private float _AlmMin;

        public float AlmMin
        {
            get
            {
                return _AlmMin;
            }

            set
            {
                _AlmMin = value;
                NotifyOfPropertyChange(nameof(AlmMin));
            }
        }

        private float _StopMax;

        public float StopMax
        {
            get
            {
                return _StopMax;
            }

            set
            {
                _StopMax = value;
                NotifyOfPropertyChange(nameof(StopMax));
            }
        }

        private float _StopMin;

        public float StopMin
        {
            get
            {
                return _StopMin;
            }

            set
            {
                _StopMin = value;
                NotifyOfPropertyChange(nameof(StopMin));
            }
        }

        public override string ToString()
        {
            return $"{ControlTarget},{SetValue},{AlmMax},{AlmMin},{StopMax},{StopMin}";
        }

        private static float ConvertToFloat(string value)
        {
            float.TryParse(value, out float retValue);
            return retValue;
        }

        public static ExtControlEntity FromJsonString(string recipeContent)
        {
            ExtControlEntity entity = new ExtControlEntity();

            var values = recipeContent.Split(',');

            if (values.Length > 0)
                entity.ControlTarget = values[0];

            if (values.Length > 1)
                entity.SetValue = ConvertToFloat(values[1]);

            if (values.Length > 2)
                entity.AlmMax = ConvertToFloat(values[2]);

            if (values.Length > 3)
                entity.AlmMin = ConvertToFloat(values[3]);

            if (values.Length > 4)
                entity.StopMax = ConvertToFloat(values[4]);

            if (values.Length > 5)
                entity.StopMin = ConvertToFloat(values[5]);

            return entity;
        }
    }
}