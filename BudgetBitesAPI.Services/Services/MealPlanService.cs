using BudgetBitesAPI.Core.Interfaces;
using BudgetBitesAPI.Core.Models;
using BudgetBitesAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BudgetBitesAPI.Services.Services
{
    public class MealPlanService : IMealPlanService
    {
        public IUnitOfWork _unitOfWork;
        public readonly IConfiguration _config;

        public MealPlanService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<IEnumerable<MealPlanModel>> UserMealPlanDetails(MealPlanModel mealDetails)
        {
            List<MealPlanModel> resultData = new List<MealPlanModel>();
            IEnumerable<MealPlanModel> resultModel = new List<MealPlanModel>();
            var mDetail = new List<MealDetails>();
            var dDetail = new List<DishDetails>();
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            conn.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[USP_UserMealPlanDetails]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("P_Username", mealDetails.Username);
            cmd.Parameters.AddWithValue("P_WeekStartDate", mealDetails.WeekStartDate);
            cmd.Parameters.AddWithValue("P_WeekEndDate", mealDetails.WeekEndDate);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ds = new DataSet();
            ada.Fill(ds);
            conn.Close();

            DataTable dtweekDates = new DataTable();
            dtweekDates.Columns.Add("WeekDate");
            dtweekDates.Columns.Add("MealType");

            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal decCost = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Cost"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dtweekDates.NewRow();
                    dr["WeekDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["PlanDate"]);
                    dr["MealType"] = Convert.ToString(ds.Tables[0].Rows[i]["MealType"]);
                    dtweekDates.Rows.Add(dr);
                }

                DataView view = new DataView(dtweekDates);
                DataTable dtDistinct = view.ToTable(true, "WeekDate", "MealType");

                for (int i = 0; i < dtDistinct.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        resultData = new List<MealPlanModel>()
                        {
                            new MealPlanModel()
                            {
                                PersonID =  Convert.ToInt32(ds.Tables[0].Rows[i]["PersonID"]),
                                Username = Convert.ToString(ds.Tables[0].Rows[i]["Username"]),
                                TotalCost = Convert.ToDecimal(ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Cost")))
                    }
                        };
                    }

                    DateTime weekDate = Convert.ToDateTime(dtDistinct.Rows[i]["WeekDate"]);
                    string strMealType = Convert.ToString(dtDistinct.Rows[i]["MealType"]) ?? "";
                    DataTable table1 = ds.Tables[0].Select("PlanDate = '" + weekDate + "'").CopyToDataTable().Select("MealType = '" + strMealType + "'").CopyToDataTable();
                    for (int k = 0; k < table1.Rows.Count; k++)
                    {
                        if(k == 0) {
                            dDetail = new List<DishDetails>();
                           // mDetail = new List<MealDetails>();
                        }
                        var mDish = new List<DishDetails>()
                        {
                            new DishDetails()
                            {
                                MealPlanID = Convert.ToInt32(table1.Rows[k]["MealPlanID"]),
                                DishName = Convert.ToString(table1.Rows[k]["DishName"]),
                                IsFavourite = Convert.ToString(table1.Rows[k]["IsFavourite"]),
                                ShortDiscription = Convert.ToString(table1.Rows[k]["ShortDiscription"]),
                                CuisineType = Convert.ToString(table1.Rows[k]["CuisineType"]),
                                Serves = Convert.ToInt32(table1.Rows[k]["Serves"]),
                                Cost = Convert.ToDecimal(table1.Rows[k]["Cost"]),
                            }
                        };
                        dDetail.AddRange(mDish);

                        if (k == (table1.Rows.Count - 1))
                        {
                            var mlstData = new List<MealDetails>()
                            {
                                new MealDetails()
                                {
                                    MealType = strMealType,
                                    PlanDate = weekDate,
                                    DishList = dDetail
                                }
                            };
                            mDetail.AddRange(mlstData);
                        }
                    }
                }
            }

            resultModel = resultData.Select(x => new MealPlanModel
            {
                PersonID = x.PersonID,
                Username = x.Username,
                MealListDetails = mDetail,
                TotalCost = x.TotalCost
            });

            return resultModel;
        }

        public async Task<IEnumerable<MealPlanModel>> SaveUserMealPlan(MealDishModel mealDetails)
        {
            List<MealPlanModel> resultData = new List<MealPlanModel>();
            IEnumerable<MealPlanModel> resultModel = new List<MealPlanModel>();
            var mDetail = new List<MealDetails>();
            var dDetail = new List<DishDetails>();
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            conn.Open();
            for (int i = 0; i < mealDetails.MealDishList.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("[dbo].[USP_InsertUserMealPlan]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("P_Username", mealDetails.Username);
                cmd.Parameters.AddWithValue("P_PlanDate", mealDetails.MealDishList[i].PlanDate);
                cmd.Parameters.AddWithValue("P_MealType", mealDetails.Mealtype);
                cmd.Parameters.AddWithValue("P_DishName", mealDetails.MealDishList[i].DishName);
                cmd.Parameters.AddWithValue("P_ShortDiscription", mealDetails.MealDishList[i].ShortDiscription);
                cmd.Parameters.AddWithValue("P_CuisineType", mealDetails.MealDishList[i].CuisineType);
                cmd.Parameters.AddWithValue("P_isFavorite", mealDetails.MealDishList[i].IsFavourite);
                cmd.Parameters.AddWithValue("P_Serves", mealDetails.MealDishList[i].Serves);
                cmd.Parameters.AddWithValue("P_TotalCost", mealDetails.MealDishList[i].Cost);
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                ds = new DataSet();
                ada.Fill(ds);
            }
            conn.Close();

            DataTable dtweekDates = new DataTable();
            dtweekDates.Columns.Add("WeekDate");
            dtweekDates.Columns.Add("MealType");

            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal decCost = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Cost"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dtweekDates.NewRow();
                    dr["WeekDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["PlanDate"]);
                    dr["MealType"] = Convert.ToString(ds.Tables[0].Rows[i]["MealType"]);
                    dtweekDates.Rows.Add(dr);
                }

                DataView view = new DataView(dtweekDates);
                DataTable dtDistinct = view.ToTable(true, "WeekDate", "MealType");

                for (int i = 0; i < dtDistinct.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        resultData = new List<MealPlanModel>()
                        {
                            new MealPlanModel()
                            {
                                PersonID =  Convert.ToInt32(ds.Tables[0].Rows[i]["PersonID"]),
                                Username = Convert.ToString(ds.Tables[0].Rows[i]["Username"]),
                                TotalCost = Convert.ToDecimal(ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Cost")))
                    }
                        };
                    }

                    DateTime weekDate = Convert.ToDateTime(dtDistinct.Rows[i]["WeekDate"]);
                    string strMealType = Convert.ToString(dtDistinct.Rows[i]["MealType"]) ?? "";
                    DataTable table1 = ds.Tables[0].Select("PlanDate = '" + weekDate + "'").CopyToDataTable().Select("MealType = '" + strMealType + "'").CopyToDataTable();
                    for (int k = 0; k < table1.Rows.Count; k++)
                    {
                        if (k == 0)
                        {
                            dDetail = new List<DishDetails>();
                            // mDetail = new List<MealDetails>();
                        }
                        var mDish = new List<DishDetails>()
                        {
                            new DishDetails()
                            {
                                MealPlanID = Convert.ToInt32(table1.Rows[k]["MealPlanID"]),
                                DishName = Convert.ToString(table1.Rows[k]["DishName"]),
                                IsFavourite = Convert.ToString(table1.Rows[k]["IsFavourite"]),
                                ShortDiscription = Convert.ToString(table1.Rows[k]["ShortDiscription"]),
                                CuisineType = Convert.ToString(table1.Rows[k]["CuisineType"]),
                                Serves = Convert.ToInt32(table1.Rows[k]["Serves"]),
                                Cost = Convert.ToDecimal(table1.Rows[k]["Cost"]),
                            }
                        };
                        dDetail.AddRange(mDish);

                        if (k == (table1.Rows.Count - 1))
                        {
                            var mlstData = new List<MealDetails>()
                            {
                                new MealDetails()
                                {
                                    MealType = strMealType,
                                    PlanDate = weekDate,
                                    DishList = dDetail
                                }
                            };
                            mDetail.AddRange(mlstData);
                        }
                    }
                }
            }

            resultModel = resultData.Select(x => new MealPlanModel
            {
                PersonID = x.PersonID,
                Username = x.Username,
                MealListDetails = mDetail,
                TotalCost = x.TotalCost
            });

            return resultModel;
        }


        public async Task<IEnumerable<MealPlanModel>> DeleteUserMealPlan(DeleteDishModel mealDetails)
        {
            List<MealPlanModel> resultData = new List<MealPlanModel>();
            IEnumerable<MealPlanModel> resultModel = new List<MealPlanModel>();
            var mDetail = new List<MealDetails>();
            var dDetail = new List<DishDetails>();
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            conn.Open();
            SqlCommand cmd = new SqlCommand("[dbo].[USP_DeleteUserMealPlan]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("P_Username", mealDetails.Username);
            cmd.Parameters.AddWithValue("P_MealPlanID", mealDetails.MealPlanID);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ds = new DataSet();
            ada.Fill(ds);
            conn.Close();

            DataTable dtweekDates = new DataTable();
            dtweekDates.Columns.Add("WeekDate");
            dtweekDates.Columns.Add("MealType");

            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal decCost = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Cost"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dtweekDates.NewRow();
                    dr["WeekDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["PlanDate"]);
                    dr["MealType"] = Convert.ToString(ds.Tables[0].Rows[i]["MealType"]);
                    dtweekDates.Rows.Add(dr);
                }

                DataView view = new DataView(dtweekDates);
                DataTable dtDistinct = view.ToTable(true, "WeekDate", "MealType");

                for (int i = 0; i < dtDistinct.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        resultData = new List<MealPlanModel>()
                        {
                            new MealPlanModel()
                            {
                                PersonID =  Convert.ToInt32(ds.Tables[0].Rows[i]["PersonID"]),
                                Username = Convert.ToString(ds.Tables[0].Rows[i]["Username"]),
                                TotalCost = Convert.ToDecimal(ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Cost")))
                    }
                        };
                    }

                    DateTime weekDate = Convert.ToDateTime(dtDistinct.Rows[i]["WeekDate"]);
                    string strMealType = Convert.ToString(dtDistinct.Rows[i]["MealType"]) ?? "";
                    DataTable table1 = ds.Tables[0].Select("PlanDate = '" + weekDate + "'").CopyToDataTable().Select("MealType = '" + strMealType + "'").CopyToDataTable();
                    for (int k = 0; k < table1.Rows.Count; k++)
                    {
                        if (k == 0)
                        {
                            dDetail = new List<DishDetails>();
                            // mDetail = new List<MealDetails>();
                        }
                        var mDish = new List<DishDetails>()
                        {
                            new DishDetails()
                            {
                                MealPlanID = Convert.ToInt32(table1.Rows[k]["MealPlanID"]),
                                DishName = Convert.ToString(table1.Rows[k]["DishName"]),
                                IsFavourite = Convert.ToString(table1.Rows[k]["IsFavourite"]),
                                ShortDiscription = Convert.ToString(table1.Rows[k]["ShortDiscription"]),
                                CuisineType = Convert.ToString(table1.Rows[k]["CuisineType"]),
                                Serves = Convert.ToInt32(table1.Rows[k]["Serves"]),
                                Cost = Convert.ToDecimal(table1.Rows[k]["Cost"]),
                            }
                        };
                        dDetail.AddRange(mDish);

                        if (k == (table1.Rows.Count - 1))
                        {
                            var mlstData = new List<MealDetails>()
                            {
                                new MealDetails()
                                {
                                    MealType = strMealType,
                                    PlanDate = weekDate,
                                    DishList = dDetail
                                }
                            };
                            mDetail.AddRange(mlstData);
                        }
                    }
                }
            }

            resultModel = resultData.Select(x => new MealPlanModel
            {
                PersonID = x.PersonID,
                Username = x.Username,
                MealListDetails = mDetail,
                TotalCost = x.TotalCost
            });

            return resultModel;
        }

    }
}
