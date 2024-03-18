using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;

public class BookController : MonoBehaviour
{
    public EndlessBook book;
    public GameObject bookObject;

    public float filpSpeed;

    public float autoFilpSpeed;

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
        }
    }

    public void toggleBook(bool bookActive){
        bookObject.SetActive(bookActive);
        
        if (bookActive) {
            book.SetState(EndlessBook.StateEnum.ClosedFront, 0);
            book.SetPageNumber(1);

            book.TurnToPage(book.LastPageNumber, EndlessBook.PageTurnTimeTypeEnum.TimePerPage, autoFilpSpeed, autoFilpSpeed);
        }
        // else {
        //     book.TurnToPage(1, EndlessBook.PageTurnTimeTypeEnum.TimePerPage, autoFilpSpeed, autoFilpSpeed);
        //     bookObject.SetActive(bookActive);
        // }
    }
}
