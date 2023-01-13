
fetch( "http://localhost:12767/api/Transaction")
  .then(res => {
    return res.json();
  })
  .then(data => {
    let tableData = "";
    data.map((values) => {
      tableData += `<tr>
        <th>${values.transactionId}</th>
        <th>${values.transactionTitle}</th>
        <th>${values.category}</th>
        <th>${values.amount}</th>
        <th>${values.transactionDescription}</th>
        <th><a href="#">Edit</a></th>
        <th><a href="#">delete</a></th>
        `;
});
    document.getElementById("transaction").innerHTML = tableData;
  }).catch((err) => {
    console.log(err);
  })
  