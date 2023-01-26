using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI.Extensions;

public class BookshelfFilter : StaticInstance<BookshelfFilter>
{
    public List<Genre> genres;
    public List<DataElement> booksElements;
    public UIHorizontalScroller uiHorizontalScroller;
    public UnityEvent endEvent;

    public void ClearFilter()
    {
        genres.Clear();
        FilterBooks();
    }

    public void FilterAdd(Genre value)
    {
        genres.Add(value);
        FilterBooks();
    }

    public void FilterRemove(Genre value)
    {
        genres.Remove(value);
        FilterBooks();
    }

    private void FilterBooks()
    {
        foreach (var bookElement in booksElements)
        {
            bookElement.gameObject.SetActive(false);
            foreach (var genre in genres)
            {
                if (bookElement.data.jsonData.DataListString[Keys.GenreKey].Contains(genre.ToString()))
                {
                    bookElement.gameObject.SetActive(true);
                }
            }
        }

        if (genres.Count == 0)
        {
            ShowAll(true);
        }

        uiHorizontalScroller.UpdateChildren();
        endEvent?.Invoke();
    }

    private void ShowAll(bool value)
    {
        foreach (var bookElement in booksElements)
        {
            bookElement.gameObject.SetActive(value);
        }
    }
}