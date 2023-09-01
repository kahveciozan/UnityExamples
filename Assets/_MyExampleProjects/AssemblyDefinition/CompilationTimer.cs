using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;

[ExecuteInEditMode]
public class CompilationTimer : MonoBehaviour
{
    private double compileStartTime;
    private bool isCompiling = false;

    private void OnEnable()
    {
        EditorApplication.update += EditorUpdate;
        CompilationPipeline.compilationStarted += CompileStarted;
    }

    private void OnDisable()
    {
        EditorApplication.update -= EditorUpdate;
        CompilationPipeline.compilationStarted -= CompileFinished;
    }

    private void EditorUpdate()
    {
        if (isCompiling)
        {
            if (!EditorApplication.isCompiling)
            {
                isCompiling = false;
                CompileFinished(null);
            }
        }
    }

    private void CompileStarted(object obj)
    {
        Debug.Log("Compile Started...");
        isCompiling = true;
        compileStartTime = EditorApplication.timeSinceStartup;
    }

    private void CompileFinished(object obj)
    {
        double compileTime = EditorApplication.timeSinceStartup - compileStartTime;
        Debug.Log("Compile Finished: " + compileTime.ToString("F2") + "s");
    }
}
