import React, { useState } from 'react';

function App() {
  return InputForm();
}

function InputForm() {
  console.log("InputForm");
  const [inputs, setInputs] = useState({});

  const handleChange = (event) => {
    const name = event.target.id;
    const value = event.target.value;
    setInputs(values => ({...values, [name]: value}))
  }

  const handleSubmit = (event) => {
    event.preventDefault();
    Fetcher();
  }

  return (
    <form onSubmit={handleSubmit}>
      <table>
        <tr>
          <td align="right">Enter your ID:</td>
          <td align="left"><input 
            type="number" 
            id="clientId" 
            value={inputs.clientId || ""} 
            onChange={handleChange}
          />
          </td>
        </tr>

        <tr>
          <td align="right">Enter loan amount:</td>
          <td align="left"><input 
            type="number"
            id="loanAmount" 
            value={inputs.loanAmount || ""} 
            onChange={handleChange}
          />
          </td>
         </tr>

        <tr>
          <td align="right">Enter period in month:</td>
          <td align="left"><input 
              type="number" 
              id="periodMonths" 
              value={inputs.periodMonths || ""} 
              onChange={handleChange}
           />
           </td>
        </tr>

        <tr>
          <td align="right">Total amount:</td>
          <td align="left"><input 
                type="text" 
                id="totalAmount" 
                disabled="disabled"
            />
          </td>
        </tr>

        <tr>
          <td align="right">Error:</td>
          <td align="left"><input 
                type="text" 
                id="errorMessage" 
                disabled="disabled"
            />
          </td>
        </tr>

      <tr>
        <input type="submit" />
      </tr>
      </table>
    </form>
  )
}

async function Fetcher()
{
  const url = "https://localhost:6060/calculateloan";

  const clientIdVal = document.getElementById('clientId').value;
  const loanAmountVal = document.getElementById('loanAmount').value;
  const periodMonthsVal = document.getElementById('periodMonths').value;
  try {
    const response = await fetch(url, {
      method: "POST",
      mode: "cors",
      headers: new Headers({
          'Accept': '*/*',
          'Content-Type': 'application/json'
      }),
      body: JSON.stringify({ clientId: clientIdVal, loanAmount: loanAmountVal, periodMonths: periodMonthsVal })
    })

    let json = await response.json();
    let totalAmount = json.result.totalAmount;
    document.getElementById('totalAmount').value = totalAmount;
    document.getElementById('totalAmount').setAttribute("value",  totalAmount);
  }
  catch (err)
  {
    let message = "Exception";//err.message
    document.getElementById('errorMessage').value = message;
    document.getElementById('errorMessage').setAttribute("value",  message);
  }
}

export default App;
