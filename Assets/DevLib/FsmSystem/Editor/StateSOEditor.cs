using System.Collections.Generic;
using System.Linq;
using DevLib.FsmSystem.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DevLib.FsmSystem.Editor
{
    [CustomEditor(typeof(StateSO))]
    
    public class StateSOEditor : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset editorView;

        private StateSO _targetData;
        
        public override VisualElement CreateInspectorGUI()
        {
            _targetData = (StateSO) target;
            VisualElement root = new VisualElement();
            
            editorView.CloneTree(root);

            FillDropdownField(root);
            
            return root;
        }

        private void FillDropdownField(VisualElement root)
        {
            DropdownField field = root.Q<DropdownField>("state-class");
            
            IEnumerable<string> choices = TypeCache.GetTypesDerivedFrom<AbstractState>()
                .Where(type => type.IsClass && !type.IsAbstract)
                .Select(type => $"{type.FullName}, {type.Assembly.GetName().Name}" );
            
            field.choices.AddRange(choices);

            if (_targetData != null &&
                !string.IsNullOrEmpty(_targetData.className) &&
                field.choices.Contains(_targetData.className))
            {
                field.value = _targetData.className; 
            }else if (_targetData != null && field.choices.Count > 0)
            {
                _targetData.className = field.choices.First();
                EditorUtility.SetDirty(_targetData);
            }
            
            AssetDatabase.SaveAssetIfDirty(_targetData);
        }
    }
}