using UnityEngine;
using TMPro;
using VersOne.Epub;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class EpubParser : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private List<string> pages = new List<string>();
    private int currentPageIndex = 0;
    private bool bookLoaded = false;

    void Start()
    {
        LoadBook();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && bookLoaded)
        {
            ShowNextPage();
        }
    }

    private void LoadBook()
    {
        EpubBook book = EpubReader.ReadBook("Assets/Epubs/pg1342-images-3.epub");

        string fullText = "";
        for (int i = 0; i < book.ReadingOrder.Count; i++)
        {
            fullText += StripHtml(book.ReadingOrder[i].Content) + "\n\n";
        }

        StartCoroutine(Paginate(fullText));
    }

    private static string StripHtml(string input)
    {
        return Regex.Replace(input, "<.*?>", string.Empty);
    }

    private IEnumerator<WaitForEndOfFrame> Paginate(string fullText)
    {
        // Temporarily hide and prepare for pagination
        text.text = "";
        yield return new WaitForEndOfFrame();

        int startIndex = 0;
        int chunkSize = 100; // Starting size for binary fit

        while (startIndex < fullText.Length)
        {
            int length = FindFittingLength(fullText, startIndex, chunkSize);
            string page = fullText.Substring(startIndex, length);
            pages.Add(page.Trim());
            startIndex += length;
        }

        currentPageIndex = 0;
        text.text = pages[currentPageIndex];
        bookLoaded = true;
    }

    private int FindFittingLength(string textSource, int startIndex, int initialChunk)
    {
        int low = 10;
        int high = Mathf.Min(10000, textSource.Length - startIndex);
        int bestFit = low;

        while (low <= high)
        {
            int mid = (low + high) / 2;
            string candidate = textSource.Substring(startIndex, Mathf.Min(mid, textSource.Length - startIndex));

            text.text = candidate;
            text.ForceMeshUpdate();
            var info = text.textInfo;

            if (info.lineCount < text.maxVisibleLines && text.isTextOverflowing == false)
            {
                bestFit = mid;
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        return bestFit;
    }

    private void ShowNextPage()
    {
        if (currentPageIndex < pages.Count - 1)
        {
            currentPageIndex++;
            text.text = pages[currentPageIndex];
        }
    }
}
