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

### Request Form Control from jQuery Datatable Ajax Server side Process

https://github.com/aungaung99/AspNetCoreDatatable/blob/ce18778bd53967c27eb82da3343535b39a1a49e8/Controllers/DatatablesController.cs#L113-L121

