using Ookii.Dialogs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 파일 경로 탐색 및 저장과 불러오기를 가능하게 함
/// </summary>
public class PathManage : MonoBehaviour
{
    VistaOpenFileDialog OpenDialog;
    Stream openStream = null;

    // 파일 정보 저장 경로
    private string gitSavePath;
    private string repositorySavePath;

    // 실제 경로
    private string gitPath = "";
    private string repositoryPath = "";

    // 경로를 표시할 텍스트
    [SerializeField] Text gitText;
    [SerializeField] Text repositoryText;

    private void Start()
    {
        gitSavePath = UnityEngine.Application.persistentDataPath + "\\git.text";
        repositorySavePath = UnityEngine.Application.persistentDataPath + "\\repository.text";

        OpenDialog = new VistaOpenFileDialog();
        OpenDialog.Filter = "All files  (*.*)|*.*";
        OpenDialog.FilterIndex = 1;
        OpenDialog.Title = "File Search";

        LoadPath(gitSavePath, ref gitPath);
        LoadPath(repositorySavePath, ref repositoryPath);

        gitText.text = gitPath;
        repositoryText.text = repositoryPath;
    }

    // 경로 정보 불러오기
    private void LoadPath(string load, ref string path)
    {
        if (File.Exists(load))
            path = File.ReadAllText(load);
    }

    // 경로 정보 저장하기
    private void SavePath(string save, string path)
    {
        File.WriteAllText(save, path);
    }

    /// <summary>
    /// 파일 경로 탐색
    /// </summary>
    /// <param name="pathText">경로를 표시할 텍스트</param>
    /// <param name="save">파일의 경로 정보를 저장할 장소</param>
    /// <param name="path">파일의 경로</param>
    public void FileSearch(Text pathText, string save, ref string path)
    {
        if (OpenDialog.ShowDialog() == DialogResult.OK)
        {
            if ((openStream = OpenDialog.OpenFile()) != null)
            {
                path = OpenDialog.FileName;
                pathText.text = gitPath;
                SavePath(save, path);
            }
        }
    }

    // 깃 경로 탐색
    public void GitSearch()
    {
        FileSearch(gitText, gitSavePath, ref gitPath);
    }

    // 깃 경로 탐색
    public void RepositorySearch()
    {
        FileSearch(repositoryText, repositorySavePath, ref repositoryPath);
    }
}