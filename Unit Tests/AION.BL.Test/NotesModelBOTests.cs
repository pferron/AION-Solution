using AION.BL.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AION.BL.Test
{
    [TestClass]
    public class NotesModelBOTests
    {
        private Mock<NoteModelBO> _bo;
        [TestInitialize]
        public void TestInitialize()
        {
            _bo = new Mock<NoteModelBO>();
        }
        [TestMethod]
        [Ignore]
        public void InsertProjectNoteSavesSuccessfully()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            NoteTypeEnum noteTypeEnum = NoteTypeEnum.PendingNotes;
            string noteComment = "test InsertProjectNoteSavesSuccessfully";
            UserIdentity updatedUser = new UserIdentity();
            DepartmentNameEnums departmentNameEnums = DepartmentNameEnums.Building;
            int projectID = 780;
            int parentID = 0;
            int id = 0;
            Note note = _bo.Object.CreateInstance(
                noteTypeEnum,
                noteComment,
                updatedUser,
                updatedUser,
                departmentNameEnums,
                projectID,
                parentID
                );
            id = _bo.Object.InsertProjectNote(note);
            note.ID = id;

            Assert.IsTrue(id > 0);
        }
    }
}
