using R3;

namespace Samples.R3Sample
{
    public class ReactivePropertySample 
    {
        // こっちが本体
        private readonly ReactiveProperty<int> _rp = new();

        // 公開するプロパティ
        public ReadOnlyReactiveProperty<int> Value => _rp;
    }
}