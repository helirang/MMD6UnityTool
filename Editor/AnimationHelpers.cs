using System;
using System.Collections.Generic;
using MMDExtensions.Tools;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AnimationHelpers
{
    [MenuItem("Assets/MMD/Create Morph Animation")]
    public static void CreateMorphAnimation()
    {
        System.GC.Collect();
        string path =
            AssetDatabase.GetAssetPath(Selection.GetFiltered<DefaultAsset>(SelectionMode.Assets).FirstOrDefault());

        if (Path.GetExtension(path).ToUpper().Contains("VMD"))
        {
            var stream = File.Open(path, FileMode.Open);

            var vmd = VMDParser.ParseVMD(stream);

            var animationClip = new AnimationClip() { frameRate = 30 };

            var delta = 1 / animationClip.frameRate;

            var keyframes = from keys in vmd.Morphs.ToLookup(
                    k => k.MorphName,
                    v => new Keyframe(v.FrameIndex * delta, v.Weight * 100))
                            select keys;

            var gameobject = Selection.GetFiltered<GameObject>(SelectionMode.TopLevel).FirstOrDefault();
            var gameObjectName = gameobject.name;
            var parentName = gameobject.transform.parent.name;

            var mesh = gameobject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            var bsCounts = mesh.blendShapeCount;
            var blendShapeNames = Enumerable.Range(0, bsCounts).ToList()
                .ConvertAll(index => mesh.GetBlendShapeName(index));

            Dictionary<string, string> blendShapeDic = new Dictionary<string, string>();

            //Dictionary Setting 
            foreach (var value in blendShapeNames)
            {
                string compareName = value.Split('.').Last();
                blendShapeDic.Add(compareName, value);
            }

            //Animation Setting
            foreach (var package in keyframes)
            {
                var name = package.Key;

                #region if you need single morph setting
                //Change the 60th function to the 58th function
                //if (package.ToArray().Length <= 1 && package.ToArray()[0].value == 0) { }
                #endregion
                if (package.ToArray().Length <= 1) { Debug.Log(package.ToArray()[0].value); }
                else
                {
                    var curve = new AnimationCurve(package.ToArray());

                    if (blendShapeDic.ContainsKey(name))
                    {
                        animationClip.SetCurve($"", typeof(SkinnedMeshRenderer),
                                $"blendShape.{blendShapeDic[name]}", curve);
                    }
                    else
                    {
                        Debug.LogWarning($"None Morph Ignored => Morph Name : {name} => Ignored for {curve.length} Keyframes");
                    }
                }
                // ================================================================================================================
            }

            AssetDatabase.CreateAsset(animationClip, path.Replace(".vmd", ".anim"));
        }
    }

    [MenuItem("Assets/MMD/Create Camera Animation")]
    public static void ExportCameraVmdToAnim()
    {
        var selected = Selection.activeObject;
        string selectPath = AssetDatabase.GetAssetPath(selected);
        if (!string.IsNullOrEmpty(selectPath))
        {
            CameraVmdAgent camera_agent = new CameraVmdAgent(selectPath);
            camera_agent.CreateAnimationClip();
            Debug.LogFormat("[{0}]:Export Camera Vmd Success!", System.DateTime.Now);
        }
        else
        {
            Debug.LogError("没有选中文件或文件夹");
        }
    }
}