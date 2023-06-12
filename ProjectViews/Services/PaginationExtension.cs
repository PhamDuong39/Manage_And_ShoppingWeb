namespace ProjectViews.Services
{
    public class PaginationExtension
    {
        public class PagedResult<T>
        {
            public List<T> Data { get; set; }
            public int TotalItems { get; set; }
            public int TotalPages { get; set; }
            public bool HasPreviousPage { get; set; }
            public bool HasNextPage { get; set; }
            public int CurrentPage { get; set; }
        }

        public static PagedResult<T> GetPagedData<T>(List<T> data, int pageNumber, int pageSize)
        {
            int totalItems = data.Count;// Tonmg so phan tu
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);// Tinh xem co tong bao nhieu trang : Tong phan tu / so phan tu moi trang
            bool hasPreviousPage = (pageNumber > 1);
            bool hasNextPage = (pageNumber < totalPages);

            // data.Skip((pageNumber - 1) * pageSize) : bo qua so phan tu ban dau trong danh sach => Take : lay so phan tu tiep theo
            List<T> pagedData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<T>
            {
                Data = pagedData,// Data output
                // Du lieu check cho btn previous and next
                TotalItems = totalItems,
                TotalPages = totalPages,
                HasPreviousPage = hasPreviousPage,
                HasNextPage = hasNextPage,
                CurrentPage = pageNumber
            };
        }
    }
}
