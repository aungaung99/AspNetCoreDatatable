# AspNet Core jQuery Datatable
 Datatable Pagination with API
 This is the jQuery Datatable Custom UI and Custom Mode. Including pagination, searching and ordering.
 Querying the 13,000 rows from MSSQL Server with linq query.
 
 ![image](https://user-images.githubusercontent.com/57518163/220984421-6d5705a2-c0aa-41ca-abd4-846f0f0f0fc8.png)
 
## API Reference

#### Get basic datatables request
```
  [GET] /api/datatables
```

#### Post Pagination

```
  [POST] /api/datatables/pagination
```
Datatables draw page by linq query skipping and taking method

#### Post Searching

```
  [POST] /api/datatables/searching
```
Datatables draw search value by linq query contain method

#### Post Searching

```
  [POST] /api/datatables/ordering
```
Datatables sort / order by linq query orderby & orderbydescending method

### Request Form Control from jQuery Datatable Ajax Server side Process

```cs
[HttpPost]
public async Task<IActionResult> OnPostAsync()
{
    string draw = Request.Form["draw"].FirstOrDefault(); 
    string start = Request.Form["start"].FirstOrDefault(); 
    string length = Request.Form["length"].FirstOrDefault(); 
    string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(); 
    string sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault(); 
    string searchValue = Request.Form["search[value]"].FirstOrDefault(); 
    int pageSize = length != null ? Convert.ToInt32(length) : 0; 
    int skip = start != null ? Convert.ToInt32(start) : 0; 
    int recordsTotal = 0;
    https://github.com/aungaung99/AspNetCoreDatatable/blob/79de708e6409e6453d750e66a1e0c042004af9d5/Controllers/DatatablesController.cs#L48
}
```

### jQuery Ajax Server Side Ajax Request
##### This script is only work for pagination and searching but data ordering doesn't work propably.

```javascript
  $('#example').DataTable({
       ajax: {
           url: '/api/...',
           type: 'POST',
           dataType: 'JSON',
       },
       processing: true,
       serverSide: true,
       columns: [
           { data: 'userId' },
           { data: 'name' }
       ]
});
```
![image](https://user-images.githubusercontent.com/57518163/220983553-e4f57825-b860-4653-b34f-823fdfbb5141.png)

##### So we need to add the column name in this script.

```javascript
  $('#example').DataTable({
       ajax: {
           url: '/api/...',
           type: 'POST',
           dataType: 'JSON',
       },
       processing: true,
       serverSide: true,
       columns: [
           { name: 'UserId', data: 'userId' },
           { name: 'Name', data: 'name' }
       ]
});
```

![image](https://user-images.githubusercontent.com/57518163/220998049-f5cb3ac5-ef9d-4414-8d5e-22dc942f3d4b.png)

##### Customizing UI with dom of datatable option

```javascript
  $('#example').DataTable({
       ajax: {
           url: '/api/...',
           type: 'POST',
           dataType: 'JSON',
       },
       processing: true,
       serverSide: true,
       dom: "<'row'<'col-sm-12'tr>>" +
                "<'row d-flex justify-content-between'<'col-auto'p><'col-auto float-end mt-2'l>>",
       columns: [
           { name: 'UserId', data: 'userId' },
           { name: 'Name', data: 'name' }
       ]
});
```

![image](https://user-images.githubusercontent.com/57518163/220998948-91dc10b3-1703-4912-a929-5eaa4ffdb41f.png)

Response JSON Value
```json
{  
  "data" : 
  [
     { 
       "userId" : "001",
       "name"   : "Mg Mg",
     },
      { 
       "userId" : "002",
       "name"   : "Ag Ag",
     }
   ]
}
```




