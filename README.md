# AspNet Core jQuery Datatable
 Datatable Pagination with API
 This is the jQuery Datatable Custom UI and Custom Mode. Including pagination, searching and ordering.
 Querying the 13,000 rows from MSSQL Server with linq query.
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
    ...
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

Response JSON Value
```json
[
    { 
      "userId" : "001",
      "name"   : "Mg Mg",
    },
     { 
      "userId" : "002",
      "name"   : "Ag Ag",
    },
]
```




