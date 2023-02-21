# AspNet Core jQuery Datatable
 Datatable Pagination with API
 This is the jQuery Datatable Custom UI and Custom Mode. Including pagination, searching and ordering.
 
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

https://github.com/aungaung99/AspNetCoreDatatable/blob/ce18778bd53967c27eb82da3343535b39a1a49e8/Controllers/DatatablesController.cs#L113-L121

### jQuery Ajax Server Side Ajax Request
##### This script is only work for pagination and searching. Data ordering doesn't work propably.

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
           { data: 'colName' },
       ],
});
```
##### So we need to add the column name in this script.

```javascript
  $('#example').DataTable({
       ajax: {
           url: '/api/datatables/pagination',
           type: 'POST',
           dataType: 'JSON',
       },
       processing: true,
       serverSide: true,
       columns: [
           { name: 'ColName', data: 'colName' },
       ],
});
```



