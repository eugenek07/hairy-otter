using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;

public class BookController : MonoBehaviour
{
    public EndlessBook book;
    public GameObject bookObject;

    public float filpSpeed;
    public float autoFilpTime = 3.0f;
    public int flipPage = 7;

    bool testActive = true;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (book.CurrentState == EndlessBook.StateEnum.ClosedFront) {
                book.SetState(EndlessBook.StateEnum.OpenMiddle);
            } else {
                book.SetState(EndlessBook.StateEnum.ClosedFront);
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && !book.IsFirstPageGroup) {
            book.SetPageNumber(book.CurrentLeftPageNumber - 2);
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && !book.IsLastPageGroup) {
            book.SetPageNumber(book.CurrentRightPageNumber + 2);
        } else if (Input.GetKeyDown(KeyCode.DownArrow) && !book.IsFirstPageGroup) {
            book.TurnToPage(book.CurrentRightPageNumber - 2, EndlessBook.PageTurnTimeTypeEnum.TimePerPage, filpSpeed);
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && !book.IsLastPageGroup) {
            book.TurnToPage(book.CurrentRightPageNumber + 2, EndlessBook.PageTurnTimeTypeEnum.TimePerPage, filpSpeed);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            testActive = !testActive;
            toggleBook(testActive);
        } else if (Input.GetKeyDown(KeyCode.T)) {
            testActive = true;
            testBook();
        } else if (Input.GetKeyDown(KeyCode.A)) {
            // InsertPageData(int pageNumber)
            // SetPageData(int pageNumber, PageData data): Sets the material for a page at the page number.
        }
    }

    public void toggleBook(bool bookActive){
        bookObject.SetActive(bookActive);
        
        if (bookActive) {
            for (int i = 0;  i < flipPage; i++){
                PageData pg = book.InsertPageData(1);
                book.SetPageData(1, pg);
            }
            book.SetState(EndlessBook.StateEnum.ClosedFront, 0);
            //book.SetPageNumber(EndlessBook.farPageNumber);
            book.TurnToPage(flipPage, EndlessBook.PageTurnTimeTypeEnum.TotalTurnTime, autoFilpTime);    
            int index = 0;
            while (index<flipPage){
                if (!book.IsTurningPages){
                    book.RemovePageData(1);
                    index++;
                }
            }
        }
        // else {
        //     book.TurnToPage(1, EndlessBook.PageTurnTimeTypeEnum.TimePerPage, autoFilpSpeed, autoFilpSpeed);
        //     bookObject.SetActive(bookActive);
        // }
    }

    public void testBook(){
        book.SetPageNumber(1);
        book.TurnForward(0.8f);
    }
}
