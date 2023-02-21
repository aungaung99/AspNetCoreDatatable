# AspNetCoreDatatable
 Datatable Pagination with API
 This is the jQuery Datatable cus
 
## API Reference

#### Get basic datatables request

```http
  [GET] /api/datatables
```

#### Post Pagination

```http
  [POST] /api/datatables/pagination
```
Datatables draw page by linq query skipping and taking method

#### Post Searching

```http
  [POST] /api/datatables/searching
```
Datatables draw search value by linq query contain method

#### Post Searching

```http
  [POST] /api/datatables/ordering
```
Datatables sort / order by linq query orderby & orderbydescending method
