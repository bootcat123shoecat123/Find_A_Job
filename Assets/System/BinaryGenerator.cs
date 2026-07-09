using System.IO;
using MasterMemory;
using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;
using UnityEditor;

public static class BinaryGenerator
{
    #if UNITY_EDITOR
    [MenuItem("Tools/Master/Generate Job Master")]
    public static void GenerateJobMaster()
    {
        GenerateBinaryFile<JobMaster>("JobMasterData");

    }
    [MenuItem("Tools/Master/Generate InterviewQuestion Master")]
    public static void GenerateInterviewQuestionMaster()
    {
        GenerateBinaryFile<InterviewQuestionMaster>("InterviewQuestionMasterData");

    }
    [MenuItem("Tools/Master/Generate InterviewReactionCard Master")]
    public static void GenerateInterviewResponeCardMaster()
    {
        GenerateBinaryFile<InterviewReactionCardMaster>("InterviewReactionCardMasterData");

    }
    [MenuItem("Tools/Master/Generate Interviewer Master")]
    public static void GenerateInterviewerMaster()
    {
        GenerateBinaryFile<InterviewerMaster>("InterviewerMasterData");

    }

    public static void GenerateBinaryFile<T>(string fileName)
    {
        var messagePackResolvers = CompositeResolver.Create(
            MasterMemoryResolver.Instance,
            StandardResolver.Instance
        );

        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        //Data type is filtered for JobMaster, InterviewQuestionMaster, and InterviewReactionCardMaster,
        DatabaseBuilder databaseBuilder = new DatabaseBuilder();
        if (typeof(T) == typeof(JobMaster))
        {
            databaseBuilder.Append(CSVFileLoad.CSVLoad<JobMaster>("Data/" + fileName));
        }
        else if (typeof(T) == typeof(InterviewQuestionMaster))
        {
            databaseBuilder.Append(CSVFileLoad.CSVLoad<InterviewQuestionMaster>("Data/" + fileName));
        }
        else if (typeof(T) == typeof(InterviewReactionCardMaster))
        {
            databaseBuilder.Append(CSVFileLoad.CSVLoad<InterviewReactionCardMaster>("Data/" + fileName));
        }
        else if (typeof(T) == typeof(InterviewerMaster))
        {
            databaseBuilder.Append(CSVFileLoad.CSVLoad<InterviewerMaster>("Data/" + fileName));
        }
        else
        {
            Debug.LogError("Unsupported data type: " + typeof(T).Name);
            return;
        }

        var binary = databaseBuilder.Build();

        // save binary to Assets/Resources/Binary/InterviewQuestionMasterData.bytes
        var path = "Assets/Resources/Binary/" + fileName + ".bytes";
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        File.WriteAllBytes(path, binary);

        Debug.Log("Build "+typeof(T).Name+" Data");
        AssetDatabase.Refresh();
    }
#endif
}
