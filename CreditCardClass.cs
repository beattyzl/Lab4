using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CreditCard
/// </summary>
public class CreditCardClass
{
    // Data Fields
    private string cardType;
    private string cardNumber;
    private string expMonth;
    private string expYear;
    private DateTime modifiedDate;

    // Constructor
    public CreditCardClass(string cardType, string cardNumber, string expMonth, string expYear)
    {
        setCardType(cardType);
        setCardNumber(cardNumber);
        setExpMonth(expMonth);
        setExpYear(expYear);
        setModifiedDate(DateTime.Now);
    }

    // Get and Set Methods for creditCardType
    public string getCardType()
    {
        return this.cardType;
    }
    public void setCardType(string cardType)
    {
        this.cardType = cardType;
    }

    //Get and Set Methods for creditCardNumber
    public string getCardNumber()
    {
        return this.cardNumber;
    }
    public void setCardNumber(string cardNumber)
    {
        this.cardNumber = cardNumber;
    }

    //Get and Set Methods for expMonth
    public string getExpMonth()
    {
        return this.expMonth;
    }
    public void setExpMonth(string expMonth)
    {
        this.expMonth = expMonth;
    }

    //Get and Set Methods for expYear
    public string getExpYear()
    {
        return this.expYear;
    }
    public void setExpYear(string expYear)
    {
        this.expYear = expYear;
    }

    //Get and Set Methods for modifiedDate
    public DateTime getModifiedDate()
    {
        return this.modifiedDate;
    }
    public void setModifiedDate(DateTime modifiedDate)
    {
        this.modifiedDate = modifiedDate;
    }
}