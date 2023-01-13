// Category

fetch( "http://localhost:12767/api/Category")
  .then(res => {
    return res.json();
  })
  .then(data => {
    let tableData = "";
    data.map((values) => {
      tableData += `<tr>
        <th>${values.categoryId}</th>
        <th>${values.categoryName}</th>
        <th>${values.limit}</th>
        <th><a href="#">Edit</a></th>
        <th><a href="#">delete</a></th>
        `;
});
    document.getElementById("category").innerHTML = tableData;
  }).catch((err) => {
    console.log(err);
  })

function add(){
fetch("http://localhost:12767/api/Category/add", {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(item)
    })
      .then(response => response.json())
      .then(() => {
        categoryId: '';
        categoryName:'';
        limit:'' ;     
      })
      .catch(error => console.error('Unable to add item.', error));
    }