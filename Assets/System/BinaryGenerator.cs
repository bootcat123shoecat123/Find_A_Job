using System.IO;
using MasterMemory;
using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;
using UnityEditor; 

public static class BinaryGenerator
{

    [MenuItem("Tools/Master/Generate InterviewQuestionMaster")]
    private static void GenerateInterviewQuestionMaster()
    {
        
        var messagePackResolvers = CompositeResolver.Create(
            MasterMemoryResolver.Instance, 
            StandardResolver.Instance 
        );

        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        
        var databaseBuilder = new DatabaseBuilder();
        databaseBuilder.Append(CSVFileLoad.CSVLoad<InterviewQuestion>("Data/InterviewQuestionMasterData"));
        var binary = databaseBuilder.Build();

        // save binary to Assets/Resources/Binary/InterviewQuestionMasterData.bytes
        var path = "Assets/Resources/Binary/InterviewQuestionMasterData.bytes";
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        File.WriteAllBytes(path, binary);
        AssetDatabase.Refresh();
    }
}