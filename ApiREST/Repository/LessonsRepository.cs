using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ApiREST
{
    public class LessonsRepository : ILessonsRepository<Lessons>
    {
        readonly MyContext _myContext;

        public LessonsRepository(MyContext context)
        {
            _myContext = context;
        }

        public Task<List<Lessons>> GetLesson(Guid uuid)
        {
            return _myContext.Lessons.Where(l => l.Lesson_id == uuid).ToListAsync();
        }

        public async Task<Guid> InsertLesson(Lessons lesson)
        {
            await _myContext.AddAsync(lesson);
            await _myContext.SaveChangesAsync();

            return lesson.Lesson_id;
        }

        public async Task<int> EditLesson(Guid uuid, Guid Subject_id, Guid Class_id, Guid Teacher_id, string Topic)
        {
            Lessons lesson = await _myContext.Lessons.FindAsync(uuid);

            if (lesson == null)
            {
                return 0;
            }
            else if (lesson.Teacher_id != Teacher_id)
            {
                return -1;
            }

            lesson.Subject_id = Subject_id;
            lesson.Class_id = Class_id;
            lesson.Teacher_id = Teacher_id;
            lesson.Topic = Topic;

            await _myContext.SaveChangesAsync();

            return 1;

        }

        public async Task<int> DeleteLesson(Guid uuid) 
        {
            Lessons lesson = await _myContext.Lessons.FindAsync(uuid);
            if(lesson == null)
            {
                return 0;
            }

            _myContext.Remove(lesson);
            await _myContext.SaveChangesAsync();

            return 1;
        }

    }
}
