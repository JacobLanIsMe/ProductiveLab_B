using ReproductiveLabDB.Models;

ReproductiveLabContext dbContext = new ReproductiveLabContext();
//var a = dbContext.OvumPickups.Where(x=>x.CourseOfTreatmentId == Guid.Parse("03AA7955-9FED-4DDD-B70D-D0478D557530")).Select(x=>x.UpdateTime.Date).FirstOrDefault();
//var b = dbContext.OvumPickups.Where(x=>x.CourseOfTreatmentId == Guid.Parse("D639F19C-9E64-4781-B08F-4C341A4B64B9")).Select(x=>x.UpdateTime.Date).FirstOrDefault();
//var c = (a - b).Days;
//Console.WriteLine(c);