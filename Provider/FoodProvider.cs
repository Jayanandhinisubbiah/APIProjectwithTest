using APIProject.Data;
using APIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Provider
{
    public class FoodProvider : IProvider
    {


        private readonly FoodContext fd;
        public FoodProvider(FoodContext fd)
        {
            this.fd = fd;
        }

        public UserList AddNewUser(UserList U)
        {
            fd.UserList.Add(U);
            fd.SaveChanges();
            return U;
        }
        #region
        //public Cart AddtoCart(int Qnt, int FoodId, int UserId)
        //{
        //    #region
        //    //var F = fd.Food.FirstOrDefault(i => i.FoodId == C.FoodId);
        //    //var  U= fd.UserList.FirstOrDefault(i => i.UserId == C.UserId);
        //    #endregion
        //    Cart C = new Cart();
        //    var F = fd.Food.FirstOrDefault(i => i.FoodId == FoodId);
        //    var U = fd.UserList.FirstOrDefault(i => i.UserId == UserId);

        //    C.UserId =UserId;
        //    C.FoodId = F.FoodId;
        //    C.Qnt = Qnt;
        //    fd.Add(C);
        //    fd.SaveChanges();
        //    return C;
        //}

        public Cart AddtoCart(Cart C)
        {
            Cart T = new Cart();
            var F = fd.Food.FirstOrDefault(i => i.FoodId == C.FoodId);
            var U = fd.UserList.FirstOrDefault(i => i.UserId == C.UserId);
            T.UserId = U.UserId;
            T.FoodId = F.FoodId;
            T.Qnt = C.Qnt;
            fd.Add(T);
            fd.SaveChanges();
            return T;
        }
        #endregion

        public OrderMaster Buy(int UserId)
        {
            return (fd.OrderMaster.FirstOrDefault(m => m.UserId == UserId));
            // List<OrderMaster> o = (from i in fd.OrderMaster
            //         where i.UserId == UserId
            //         select i).ToList();
            //return o;
        }

        public Cart Delete(int CartId)
        {
            return (fd.Cart.FirstOrDefault(m => m.CartId == CartId));

        }

        public void DeleteConfirmed(int CartId)
        {
            //var val = ;
            fd.Cart.Remove(fd.Cart.Find(CartId));
            fd.SaveChanges();
        }

        public void EmptyList(int UserId)
        {
            List<Cart> list = (from i in fd.Cart
                               where i.UserId == UserId
                               select i).ToList();
            foreach (var item in list)
            {
                var val = fd.Cart.Find(item.CartId);
                fd.Cart.Remove(val);
                fd.SaveChanges();
            }
        }

        public List<Food> GetAll()
        {
            return fd.Food.ToList();
        }

        public List<Cart> GetCartById(int UserId)
        {
            return (from i in fd.Cart.Include(x => x.Food)
                    where i.UserId == UserId
                    select i).ToList();


        }

        public Food GetFoodById(int? id)
        {
            return (fd.Food.Find(id));
        }

        public UserList Login(UserList U)
        {
            var result = (from i in fd.UserList
                          where i.FName == U.FName && i.Password == U.Password && i.Role == U.Role
                          select i).SingleOrDefault();

            return result;

        }


        public OrderMaster Pay(int OrderId)
        {
            var result = fd.OrderMaster.SingleOrDefault(m => m.OrderId == OrderId);
            return result;
        }


        public void Pay(int OrderId, OrderMaster O)
        {
            var result = fd.OrderMaster.SingleOrDefault(m => m.OrderId == OrderId);
            result.BankName = O.BankName;
            result.CardNo = O.CardNo;
            result.CCV = O.CCV;
            fd.SaveChanges();
        }
        public List<OrderDetails> OrderDetails()
        {
            var C = fd.OrderDetails.ToList();
            return C;
        }

        //public void Payment(int OrderId, string Type)
        //{
        //    var result = fd.OrderMaster.SingleOrDefault(m => m.OrderId == OrderId);
        //    result.Type = Type;
        //    fd.SaveChanges();
        //}
        public OrderMaster Payment(int OrderId, string Type)
        {
            var result = fd.OrderMaster.SingleOrDefault(m => m.OrderId == OrderId);
            result.Type = Type;
            fd.SaveChanges();
            return result;
        }

        public void ViewCart(int? UserId)
        {

            List<Cart> list = (from i in fd.Cart
                               where i.UserId == UserId
                               select i).ToList();
            OrderMaster orderMaster = new OrderMaster();


            orderMaster.UserId = UserId;
            fd.Add(orderMaster);
            fd.SaveChanges();


            List<OrderDetails> orderDetails = new List<OrderDetails>();
            foreach (var item in list)
            {

                var F = fd.Food.SingleOrDefault(i => i.FoodId == item.FoodId);


                OrderDetails od = new OrderDetails();
                od.OrderId = orderMaster.OrderId;
                od.FoodId = item.FoodId;
                od.Qnt = item.Qnt;
                od.Price = F.price;
                od.TotalPrice = od.Qnt * od.Price;
                fd.OrderDetails.Add(od);
                fd.SaveChanges();


            }
            orderDetails.AddRange(fd.OrderDetails);
            orderMaster.TotalPrice = orderDetails.Sum(i => i.TotalPrice);
            fd.SaveChanges();
        }
        public Food AddNewFood(Food food)
        {
            fd.Add(food);
            fd.SaveChanges();
            return food;
        }

        public void Edit(int CartId, Cart C)
        {
            fd.Cart.Update(C);
            fd.SaveChanges();
        }

        public Cart GetCartByCartId(int CartId)
        {
            return (fd.Cart.Find(CartId));
        }

        public void DeleteCart(int CartId)
        {
            Cart c = fd.Cart.Find(CartId);
            fd.Remove(c);
            fd.SaveChanges();
        }

        public void EditFood(int id, Food food)
        {
            //if (id != food.FoodId)
            //{
            //    return BadRequest();
            //}

            //fd.Entry(food).State = EntityState.Modified;
            fd.Update(food);

            //try
            //{
            fd.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!FoodExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
        }
        public void DeleteFood(int FoodId)
        {
            Food c = fd.Food.Find(FoodId);
            fd.Remove(c);
            fd.SaveChanges();
        }
        public List<UserList> UserDetails()
        {
            var C = (from i in fd.UserList
                     where i.Role == "User"
                     select i).ToList();
            return C;
        }

        public List<Content> GetReportById(int? UserId)
        {

            var O = (from i in fd.OrderMaster
                     where i.UserId == UserId
                     select i).ToList();
            List<Content> ct = new List<Content>();
            foreach (var k in O)
            {
                List<OrderDetails> list =
                (from i in fd.OrderDetails
                 where i.OrderId == k.OrderId
                 select i).ToList();
                //}
                //Content content = new Content();

                foreach (var item in list)
                {

                    var T = fd.Food.SingleOrDefault(i => i.FoodId == item.FoodId);
                    var Y = fd.OrderDetails.SingleOrDefault(i => i.FoodId == item.FoodId);
                    var F = fd.UserList.Find(UserId);

                    Content od = new Content();


                    od.Email = F.Email;

                    od.FoodName = T.FoodName;
                    od.Image = T.Image;
                    od.Qnt = Y.Qnt;
                    od.Price = Y.Price;

                    od.TotalPrice = Y.TotalPrice;
                    fd.SaveChanges();
                    ct.Add(od);
                };

            }

            fd.SaveChanges();


            return ct;

        }

        public List<NewOrder> ViewNewOrder()
        {
            List<OrderDetails> list = fd.OrderDetails.ToList();

            List<NewOrder> ct = new List<NewOrder>();


            foreach (var item in list)
            {

                var T = fd.Food.SingleOrDefault(i => i.FoodId == item.FoodId);
                var Y = fd.OrderDetails.SingleOrDefault(i => i.FoodId == item.FoodId);
                //var F=(from i in fd.OrderMaster
                //      where i.OrderId==item.OrderId
                //      select i.User.Email);
                var F = fd.OrderMaster.SingleOrDefault(i => i.OrderId == item.OrderId);
                var L = fd.UserList.Find(F.UserId);
                NewOrder od = new NewOrder();

                od.count = Y.Id;
                od.Email = L.Email;

                od.FoodName = T.FoodName;
                od.Image = T.Image;
                od.Qnt = Y.Qnt;
                od.Price = Y.Price;

                od.TotalPrice = Y.TotalPrice;
                fd.SaveChanges();
                ct.Add(od);
            };



            fd.SaveChanges();


            return ct;

        }
    }
}
    












