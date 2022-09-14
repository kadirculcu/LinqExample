using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExample
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Arraylarle çalışmak
            int[] tumRakamlar = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var rakam = tumRakamlar.Where(x => x % 2 == 0);
            var rakams = tumRakamlar.Where(x => x % 2 == 0).Select(x => x);

            //Nesnelerle Çalışmak

            //Nesneye değer atama
            var teacherNesne = new TeacherDto()
            {
                Tad = "Mali"
            };

            List<Student> student = new List<Student>();

            // Listeye ekleme yolları 
            Student student1 = new Student();
            student1.Ad = "Kadir";
            student1.Soyad = "Çulcu";
            student1.No = 24;
            student1.Sınıf = 2;
            student1.Yas = 26;
            student.Add(student1);

            //Listeye ekleme yolları
            student.Add(new Student()
            {
                Ad = "Özgür",
                Soyad = "İnan",
                No = 22,
                Sınıf = 2,
                Yas = 27
            });

            student.Add(new Student()
            {
                Ad = "Murat",
                Soyad = "Kap",
                No = 25,
                Sınıf = 1,
                Yas = 28
            });

            student.Add(new Student()
            {
                Ad = "Safa",
                Soyad = "Yılmaz",
                No = 23,
                Sınıf = 1,
                Yas = 26
            });


            student.Add(new Student() { Ad = "Şamil", Soyad = "Çulcu", No = 234, Sınıf = 2, Yas = 2 });
            student.Add(new Student() { Ad = "Ayhan", Soyad = "Arslan", No = 235, Sınıf = 1, Yas = 26 });
            student.Add(new Student() { Ad = "Akın", Soyad = "Conba", No = 236, Sınıf = 2, Yas = 26 });
            student.Add(new Student() { Ad = "Akın", Soyad = "Conba", No = 236, Sınıf = 2, Yas = 27 });
            student.Add(new Student() { Ad = "Şef", Soyad = "Coban", No = 42, Sınıf = 5, Yas = 27 });
            student.Add(new Student() { Ad = "Taylan", Soyad = "Kaya", No = 17, Sınıf = 5, Yas = 28 });

            List<Teacher> teachers = new List<Teacher>();
            teachers.Add(new Teacher() { Tad = "Ali", TSoyad = "Veli", Sınıf = 2 });
            teachers.Add(new Teacher() { Tad = "Mustafa", TSoyad = "elli", Sınıf = 2 });

            //Linq Join

            var query = from st in student
                        join tc in teachers on st.Sınıf equals tc.Sınıf
                        where st.Yas == 26
                        select new
                        {
                            StudentName = st.Ad,
                            TeacherName = tc.Tad
                        };

            var query2 = from st in student
                        join tc in teachers on st.Sınıf equals tc.Sınıf
                        select new
                        {
                            StudentName = st.Ad,
                            TeacherName = tc.Tad,
                        };


            var sorgu = from St in student
                        where St.Sınıf == 2
                        select St;


            var ilkDeger = (from St in student
                            where St.Sınıf == 2
                            select St.Ad).FirstOrDefault();


            //lambdaJoin
            var lamdaJoin = student.Join(teachers, x => x.Sınıf, y => y.Sınıf, (x, y) => new { x.Ad, x.Soyad, y.Tad })
                  .Select(x => new TeacherDto
                  {
                      Tad = x.Tad
                  });
            var lamdaJoin2 = student.Join(teachers, x => x.Sınıf, y => y.Sınıf, (x, y) => new { x.Ad, x.Soyad, y.Tad });

            // Lambda
            //Listeden string Listesine kayıt Atma
            var teacherNames = query.Where(x => x.StudentName == "Kadir")
                .Select(x => x.TeacherName);

            //Listeden liste parametresi ile Select yapmı
            //Birden çok değeri karşılaştırmak için kullanılır.
            List<int> param = new List<int>() { 1, 2, 3, 4 };
            var ogr = student.Where(x => param.Contains(x.Sınıf)).Select(x => x);
            var ogrSecond = student.Where(x => param.Contains(x.Sınıf) && param.Contains(x.Yas)).Select(x => x);

            //Listeden Nesne Listesine Kayıt Atma
            var teachernameDtos = query.Where(x => x.StudentName == "Kadir")
                  .Select(x => new TeacherDto()
                  {
                      Tad = x.TeacherName
                  });

            //Listeden Listeye mapping
            List<TeacherDto> listMapping = new List<TeacherDto>();
            foreach (var item in query)
            {
                var dto = new TeacherDto()
                {
                    Tad = item.TeacherName
                };
                listMapping.Add(dto);
            }

            //Listeden Nesneye Kayıt Atma
            var teacherDto = new TeacherDto()
            {
                // Tad = query.FirstOrDefault(x=>x.StudentName=="Kadir").TeacherName
                // Tad = query.Select(x=>x.TeacherName).FirstOrDefault()
                Tad = query.Where(x => x.StudentName == "Kadir").Select(x => x.TeacherName).FirstOrDefault()
            };

            //Nesne Listesinden Nesneye
            var objectListTeacherDto = new TeacherDto()
            {
                Tad = teachernameDtos.Select(x => x.Tad).FirstOrDefault()
                // Tad = teachernameDtos.FirstOrDefault().Tad
                // Tad = listMapping.FirstOrDefault().Tad
            };

            // String Listesinden Dtoya
            var srtingTeacherDto = new TeacherDto()
            {
                // Tad = teacherNames.FirstOrDefault()
                // Tad = teacherNames.LastOrDefault()
                Tad = teacherNames.Select(x => x).FirstOrDefault()
            };

            //Nesneden Nesneye Mapping
            var objectMapping = new TeacherDto()
            {
                Tad = objectListTeacherDto.Tad
            };


            string sayı = DateTime.Now.ToString();

            TimeSpan date;
            date =new TimeSpan(9,0,0);
            if (date > new TimeSpan(8, 0, 0))
            {
                 sayı = DateTime.Now.ToString() + "03";
            }

            int d = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString());
            int d1 = Convert.ToInt32(DateTime.Today.ToString("yyyyMM"));

            Console.WriteLine(query);

           //Linqde kullanılam yöntemler için uygulamalar artacak

        }
    }
}
