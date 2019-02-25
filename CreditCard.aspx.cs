using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class CreditCard : System.Web.UI.Page
{
    System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString());

    // Declare global variable for selected Credit Card ID assigned in DropDownListSelectCreditCardID_SelectedIndexChanged method
    string tempID = "";
    // Declare static global variable for whether or not Credit Card ID was selected from DropDownListSelectedCreditCardID
    static Boolean creditCardSelected;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    // Commit Button action
    protected void ButtonCommit_Click(object sender, EventArgs e)
    {
        // Create new object of Credit Card Class
        CreditCardClass tempCreditCard = new CreditCardClass(DropDownListCardType.Text, HttpUtility.HtmlEncode(TextBoxCardNumber.Text), DropDownListExpMonth.Text, DropDownListExpYear.Text);
        
        // Store selected credit card id value into variable
        string tempID = DropDownListSelectCreditCardID.SelectedValue;

        sc.Open();
        //TextBoxResult.Text = creditCardSelected.ToString();
        if (creditCardSelected == true)
        {
            // Edit credit card information
            String query3 = "UPDATE [Sales].[CreditCard] SET [CardType] = @cardtype, [CardNumber] = @cardnumber, [ExpMonth] = @expmonth, [ExpYear] = @expyear, [ModifiedDate] = @modifieddate WHERE [CreditCardID] = @tempid;";
            System.Data.SqlClient.SqlCommand sqlCom3 = new System.Data.SqlClient.SqlCommand(query3, sc);
            sqlCom3.Parameters.AddWithValue("@cardtype", tempCreditCard.getCardType());
            sqlCom3.Parameters.AddWithValue("@cardnumber", tempCreditCard.getCardNumber());
            sqlCom3.Parameters.AddWithValue("@expmonth", tempCreditCard.getExpMonth());
            sqlCom3.Parameters.AddWithValue("@expyear", tempCreditCard.getExpYear());
            sqlCom3.Parameters.AddWithValue("@modifieddate", tempCreditCard.getModifiedDate());
            sqlCom3.Parameters.AddWithValue("@tempid", tempID);
            sqlCom3.ExecuteNonQuery();

            TextBoxResult.Text = "Credit card information updated.";
        }
        else if (creditCardSelected == false)
        {
            //Insert new credit card information
            String query1 = "INSERT INTO [Sales].[CreditCard] VALUES (@cardtype, @cardnumber, @expmonth, @expyear, @modifieddate);";
            System.Data.SqlClient.SqlCommand sqlCom1 = new System.Data.SqlClient.SqlCommand(query1, sc);
            sqlCom1.Parameters.AddWithValue("@cardtype", tempCreditCard.getCardType());
            sqlCom1.Parameters.AddWithValue("@cardnumber", tempCreditCard.getCardNumber());
            sqlCom1.Parameters.AddWithValue("@expmonth", tempCreditCard.getExpMonth());
            sqlCom1.Parameters.AddWithValue("@expyear", tempCreditCard.getExpYear());
            sqlCom1.Parameters.AddWithValue("@modifieddate", tempCreditCard.getModifiedDate());
            sqlCom1.ExecuteNonQuery();

            TextBoxResult.Text = "Credit card information committed.";
            DropDownListSelectCreditCardID.DataBind();
        }

        // Reset creditCardSelected boolean back to false
        creditCardSelected = false;

        //Clear textbox fields
        DropDownListCardType.ClearSelection();
        TextBoxCardNumber.Text = "";
        DropDownListExpMonth.ClearSelection();
        DropDownListExpYear.ClearSelection();
        //Clear Update Credit Card ID Selection
        DropDownListSelectCreditCardID.ClearSelection();
    }

    // Clear Button action
    protected void ButtonClear_Click(object sender, EventArgs e)
    {
        DropDownListCardType.ClearSelection();
        TextBoxCardNumber.Text = "";
        DropDownListExpMonth.ClearSelection();
        DropDownListExpYear.ClearSelection();
        TextBoxResult.Text = "";
        // Clear Update Credit Card ID Selection
        DropDownListSelectCreditCardID.ClearSelection();
    }

    // Exit button action
    protected void ButtonExit_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
    }

    // Select a Credit Card ID record to edit
    protected void DropDownListSelectCreditCardID_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Store selected credit card id value into variable
        string tempID = DropDownListSelectCreditCardID.SelectedValue;
        creditCardSelected = true;

        // Create temporary variables to store selected record information
        string tempCardType = "";
        string tempCardNumber = "";
        Byte tempExpMonth = 0;
        Int16 tempExpYear = 0;

        // Select record from database at selected credit card ID value
        sc.Open();
        String query2 = "SELECT * FROM [Sales].[CreditCard] WHERE [CreditCardID] = @tempid;";
        System.Data.SqlClient.SqlCommand sqlCom2 = new System.Data.SqlClient.SqlCommand(query2, sc);
        sqlCom2.Parameters.AddWithValue("@tempid", tempID);
        sqlCom2.ExecuteNonQuery();

        // Use Reader and assign temporary variables with data from selected record
        System.Data.SqlClient.SqlDataReader reader = sqlCom2.ExecuteReader();
        while (reader.Read())
        {
            tempCardType = reader.GetString(1);
            tempCardNumber = reader.GetString(2);
            tempExpMonth = reader.GetByte(3);
            tempExpYear = reader.GetInt16(4);
        }
        reader.Close();

        //Display selected data in the textboxes in the web form 
        DropDownListCardType.SelectedValue = DropDownListCardType.Items.FindByText(tempCardType).Value;
        TextBoxCardNumber.Text = tempCardNumber;
        DropDownListExpMonth.SelectedValue = DropDownListExpMonth.Items.FindByValue(tempExpMonth.ToString()).Value;
        DropDownListExpYear.SelectedValue = DropDownListExpYear.Items.FindByValue(tempExpYear.ToString()).Value;

        TextBoxResult.Text = creditCardSelected.ToString();
    }
    
}