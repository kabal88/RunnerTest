﻿using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Identifier;
using Interfaces;
using Models;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;

namespace Descriptions
{
    [Serializable]
    public class InputDescription : IInputDescription
    {
        [SerializeField] private InputDescriptionIdentifier _id;
        [SerializeField] private InputActionAsset actions;

        [SerializeField, ListDrawerSettings(ShowPaging = false)]
        private List<InputActionSettings> inputActionSettings = new List<InputActionSettings>();

        public int Id => _id.Id;
        public InputActionsModel Model => new(actions, inputActionSettings);

        #region UnityEditor

#if UNITY_EDITOR
        private string savePath => "Assets/" + "/Data/" + "Identifiers/" + "InputIdentifiers/";

        [Button]
        private void FillInputActions()
        {
            var hashTest = new HashSet<string>();

            foreach (var map in actions.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    var check = inputActionSettings.FirstOrDefault(x => x.ActionName == action.name);

                    if (check == null)
                        inputActionSettings.Add(new InputActionSettings { ActionName = action.name });

                    hashTest.Add(action.name);
                }
            }

            foreach (var inputAction in inputActionSettings.ToArray())
            {
                if (hashTest.Contains(inputAction.ActionName)) continue;
                else inputActionSettings.Remove(inputAction);
            }

            CreateAndFillIdentifiers();
        }

        private void CreateAndFillIdentifiers()
        {
            var inputIdentifiers = AssetDatabase.FindAssets("t:InputIdentifier")
                .Select(x => UnityEditor.AssetDatabase.GUIDToAssetPath(x))
                .Select(x => UnityEditor.AssetDatabase.LoadAssetAtPath<InputIdentifier>(x)).ToList();

            CheckFolder(savePath);

            foreach (var inputAction in inputActionSettings)
            {
                if (inputAction.Identifier == null || inputAction.Identifier.name != inputAction.ActionName)
                {
                    var neededIdentifier = inputIdentifiers.FirstOrDefault(x => x.name == inputAction.ActionName);

                    if (neededIdentifier != null)
                    {
                        inputAction.Identifier = neededIdentifier;
                        continue;
                    }

                    inputAction.Identifier = CreateIdentifier(inputAction.ActionName);
                }
            }

            AssetDatabase.SaveAssets();
        }

        private InputIdentifier CreateIdentifier(string name)
        {
            var identifier = ScriptableObject.CreateInstance<InputIdentifier>();
            identifier.name = name;

            SaveIdentifier(identifier);
            return identifier;
        }

        private void SaveIdentifier(InputIdentifier inputIdentifier)
        {
            AssetDatabase.CreateAsset(inputIdentifier, savePath + $"{inputIdentifier.name}.asset");
        }

        private static void CheckFolder(string path)
        {
            var folder = new DirectoryInfo(path);

            if (folder == null || !folder.Exists)
                Directory.CreateDirectory(path);
        }

#endif

        #endregion
    }

    [Serializable]
    public class InputActionSettings
    {
        [ReadOnly] public string ActionName;

        [ReadOnly] public InputIdentifier Identifier;

        public InputAction InputAction;
    }

    public class ReadOnlyList<T> : IReadOnlyList<T>
    {
        private List<T> list;

        public T this[int index] => list[index];

        public int Count => list.Count;

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();

        public ReadOnlyList(List<T> list)
        {
            this.list = list;
        }
    }
}