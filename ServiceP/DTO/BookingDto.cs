﻿namespace ServiceP.DTO
{
    public class BookingDto
    {
        public int serviceId { get; set; }
        public int quantity { get; set; }
    }
    public class BookingUpdateDto
    {
        public int quantity { get; set; }
    }
}
