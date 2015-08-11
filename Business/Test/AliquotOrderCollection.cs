﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test
{
	public class AliquotOrderCollection : ObservableCollection<AliquotOrder>
	{
		public AliquotOrderCollection()
		{

		}

		public AliquotOrder GetNextItem(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, string aliquotType, DateTime accessionDate)
		{
			string aliquotOrderId = null;
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

			if (accessionDate >= DateTime.Parse("1/1/2015"))
			{
				aliquotOrderId = this.GetNextId(specimenOrder.SpecimenOrderId, aliquotType);
			}
			else
			{
				aliquotOrderId = objectId;
			}

			AliquotOrder aliquotOrder = new AliquotOrder(aliquotOrderId, objectId, specimenOrder.SpecimenOrderId);
			return aliquotOrder;
		}

		public string GetNextId(string specimenOrderId, string aliquotType)
		{            			
			string result = OrderIdParser.GetNextAliquotOrderId(this, specimenOrderId, aliquotType);
			return result;
		}

        public YellowstonePathology.Business.Interface.IOrderTarget GetOrderTarget(string orderTargetId)
        {
            YellowstonePathology.Business.Interface.IOrderTarget result = null;
            foreach (AliquotOrder aliquotOrder in this)
            {
                if (aliquotOrder.AliquotOrderId == orderTargetId)
                {
                    result = aliquotOrder;
                    break;
                }
            }
            return result;
        }

        public AliquotOrder GetByAliquotOrderId(string aliquotOrderId)
        {
            foreach (AliquotOrder item in this)
            {
                if (item.AliquotOrderId == aliquotOrderId)
                {
                    return item;
                }
            }
            return null;
        }

        public AliquotOrder GetCytycSlide()
        {
            AliquotOrder result = null;
            foreach (AliquotOrder item in this)
            {
                if (item.AliquotType == "Cytyc Slide")
                {
                    result = item;
                }
            }
            return result;
        }

		public AliquotOrder GetByTestOrderId(string testOrderId)
		{
			AliquotOrder result = null;			
			foreach (AliquotOrder aliquotOrder in this)
			{
                if (aliquotOrder.TestOrderCollection.Exists(testOrderId) == true)
                {
                    result = aliquotOrder;
                    break;
                }
			}
			return result;
		}

		// WHC this is not currently used
		public void VerifyGross(string aliquotOrderId, int verifyingUserId)
		{
			foreach (AliquotOrder item in this)
			{
				if (item.AliquotOrderId == aliquotOrderId)
				{
					item.GrossVerified = true;
					item.GrossVerifiedById = verifyingUserId;
					item.GrossVerifiedDate = DateTime.Now;
				}
			}
		}

		public AliquotOrder AddBlock(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, string aliquotLabelType, DateTime accessionDate)
		{
            AliquotOrder aliquotOrder = this.GetNextItem(specimenOrder, AliquotType.Block, accessionDate);            
			aliquotOrder.SpecimenOrderId = specimenOrder.SpecimenOrderId;
			aliquotOrder.Description = string.Empty;
			aliquotOrder.AliquotType = "Block";
			aliquotOrder.LabelPrefix = string.Empty;
            aliquotOrder.LabelType = aliquotLabelType;

			this.Add(aliquotOrder);
			this.SetBlockLabels(specimenOrder.SpecimenNumber);

			return aliquotOrder;
		}

		public AliquotOrder AddFNASlide(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, int passNumber, int slideNumber, DateTime accessionDate)
        {
			AliquotOrder aliquotOrder = this.GetNextItem(specimenOrder, AliquotType.FNA, accessionDate);
            aliquotOrder.SpecimenOrderId = specimenOrder.SpecimenOrderId;
            aliquotOrder.Description = string.Empty;
            aliquotOrder.AliquotType = "FNASLD";
            aliquotOrder.LabelPrefix = string.Empty;
            aliquotOrder.Label = "P" + passNumber.ToString() + "S" + slideNumber;
            aliquotOrder.LabelType = YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel;
            this.Add(aliquotOrder);
            this.SetBlockLabels(specimenOrder.SpecimenNumber);
            return aliquotOrder;
        }

		public AliquotOrder AddNGYNSlide(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, DateTime accessionDate)
        {
            AliquotOrder aliquotOrder = this.GetNextItem(specimenOrder, AliquotType.FNA, accessionDate);
            aliquotOrder.SpecimenOrderId = specimenOrder.SpecimenOrderId;
            aliquotOrder.Description = string.Empty;
            aliquotOrder.AliquotType = "NGYNSLD";
            aliquotOrder.LabelPrefix = string.Empty;
            aliquotOrder.Label = specimenOrder.SpecimenNumber.ToString() + ".Non-Gyn";
            aliquotOrder.LabelType = YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel;
            this.Add(aliquotOrder);
            this.SetBlockLabels(specimenOrder.SpecimenNumber);
            return aliquotOrder;
        }

		public AliquotOrder AddCESlide(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, DateTime accessionDate)
        {
            AliquotOrder aliquotOrder = this.GetNextItem(specimenOrder, AliquotType.Slide, accessionDate);
            aliquotOrder.SpecimenOrderId = specimenOrder.SpecimenOrderId;
            aliquotOrder.Description = string.Empty;
            aliquotOrder.AliquotType = "Slide";
            aliquotOrder.LabelPrefix = string.Empty;
            aliquotOrder.Label = specimenOrder.SpecimenNumber.ToString() + ".CE";
            aliquotOrder.LabelType = YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel;
            this.Add(aliquotOrder);
            this.SetBlockLabels(specimenOrder.SpecimenNumber);
            return aliquotOrder;
        }

		public AliquotOrder AddFNASlide(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, int slideNumber, DateTime accessionDate)
        {
            AliquotOrder aliquotOrder = this.GetNextItem(specimenOrder, AliquotType.FNA, accessionDate);
            aliquotOrder.SpecimenOrderId = specimenOrder.SpecimenOrderId;
            aliquotOrder.Description = string.Empty;
            aliquotOrder.AliquotType = "FNASLD";
            aliquotOrder.LabelPrefix = string.Empty;
            aliquotOrder.Label = specimenOrder.SpecimenNumber + "." + slideNumber;
            aliquotOrder.LabelType = YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel;
            this.Add(aliquotOrder);
            this.SetBlockLabels(specimenOrder.SpecimenNumber);
            return aliquotOrder;
        }

		public AliquotOrder AddFrozenBlock(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, string aliquotLabelType, DateTime accessionDate)
		{
			AliquotOrder aliquotOrder = this.GetNextItem(specimenOrder, AliquotType.FrozenBlock, accessionDate);
			aliquotOrder.SpecimenOrderId = specimenOrder.SpecimenOrderId;
			aliquotOrder.Description = string.Empty;
			aliquotOrder.AliquotType = "FrozenBlock";
			aliquotOrder.LabelPrefix = "FS";
            aliquotOrder.LabelType = aliquotLabelType;
			this.Add(aliquotOrder);
			this.SetBlockLabels(specimenOrder.SpecimenNumber);
			return aliquotOrder;
		}

		public AliquotOrder AddCellBlock(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, string aliquotLabelType, DateTime accessiondate)
		{
			AliquotOrder aliquotOrder = this.GetNextItem(specimenOrder, AliquotType.CellBlock, accessiondate);
			aliquotOrder.SpecimenOrderId = specimenOrder.SpecimenOrderId;
			aliquotOrder.Description = string.Empty;
			aliquotOrder.AliquotType = "CellBlock";
			aliquotOrder.LabelPrefix = "CB";
            aliquotOrder.LabelType = aliquotLabelType;
			this.Add(aliquotOrder);
			this.SetBlockLabels(specimenOrder.SpecimenNumber);
			return aliquotOrder;
		}

		public AliquotOrder AddSlide(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, string aliquotLabelType, DateTime accessionDate)
		{
			AliquotOrder aliquotOrder = this.GetNextItem(specimenOrder, AliquotType.Slide, accessionDate);
			aliquotOrder.SpecimenOrderId = specimenOrder.SpecimenOrderId;
			aliquotOrder.Description = string.Empty;
			aliquotOrder.AliquotType = "Slide";
			aliquotOrder.LabelPrefix = string.Empty;
            aliquotOrder.LabelType = aliquotLabelType;
			this.Add(aliquotOrder);
			this.SetSlideLabels(specimenOrder.SpecimenNumber);
			return aliquotOrder;
		}

		public AliquotOrder AddCytycSlide(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, DateTime accessionDate)
        {
            AliquotOrder aliquotOrder = this.GetNextItem(specimenOrder, AliquotType.Slide, accessionDate);
            aliquotOrder.SpecimenOrderId = specimenOrder.SpecimenOrderId;
            aliquotOrder.Description = string.Empty;
            aliquotOrder.AliquotType = "Cytyc Slide";
            aliquotOrder.LabelPrefix = string.Empty;
            aliquotOrder.LabelType = "Slide";
            this.Add(aliquotOrder);
            this.SetSlideLabels(specimenOrder.SpecimenNumber);
            return aliquotOrder;
        }


		public AliquotOrder AddSpecimen(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, string aliquotLabelType, DateTime accessiondate)
		{
			AliquotOrder aliquotOrder = this.GetNextItem(specimenOrder, AliquotType.Specimen, accessiondate);
			aliquotOrder.SpecimenOrderId = specimenOrder.SpecimenOrderId;
			aliquotOrder.Description = specimenOrder.Description;
			aliquotOrder.AliquotType = "Specimen";
			aliquotOrder.LabelPrefix = string.Empty;
            aliquotOrder.LabelType = aliquotLabelType;
			this.Add(aliquotOrder);
			aliquotOrder.Label = specimenOrder.Description;
			return aliquotOrder;
		}

		private void SetBlockLabels(int specimenNumber)
		{
			YellowstonePathology.Business.Specimen.Model.AliquotLetterList aliquotLetterList = new YellowstonePathology.Business.Specimen.Model.AliquotLetterList();

			int count = 0;
			for (int idx = 0; idx < this.Count; idx++)
			{
				if (this[idx].IsBlock() == true)
				{
					this[idx].Label = specimenNumber.ToString() + aliquotLetterList[count];
					count++;
				}
			}
		}

		private void SetSlideLabels(int specimenNumber)
		{
			int count = 1;
			for (int idx = 0; idx < this.Count; idx++)
			{
				if (this[idx].IsSlide() == true)
				{
					this[idx].Label = specimenNumber.ToString() + "." + count.ToString();
					count++;
				}
			}
		}

		public bool CanRemoveAliquot(string aliquotOrderId)
		{
			bool result = false;

			AliquotOrder aliquotOrder = this.GetByAliquotOrderId(aliquotOrderId);
			if (aliquotOrder.IsSpecimen() == true)
			{
				result = true;
			}
			else if (aliquotOrder.IsBlock() == true)
			{
				if (this[Count - 1].AliquotOrderId == aliquotOrderId)
				{
					result = true;
				}
				else
				{
					for (int idx = this.Count - 1; idx >= 0; idx--)
					{
						if (this[idx].IsBlock() == true)
						{
							if (this[idx].AliquotOrderId == aliquotOrderId)
							{
								result = true;
							}
							break;
						}
					}
				}
			}
			else if (aliquotOrder.IsSlide() == true)
			{
				if (this[Count - 1].AliquotOrderId == aliquotOrderId)
				{
					result = true;
				}
				else
				{
					for (int idx = this.Count - 1; idx >= 0; idx--)
					{
						if (this[idx].IsSlide() == true)
						{
							if (this[idx].AliquotOrderId == aliquotOrderId)
							{
								result = true;
							}
							break;
						}
					}
				}
			}

			return result;
		}

		public AliquotOrderCollection GetUnPrintedBlocks()
		{
			AliquotOrderCollection result = new AliquotOrderCollection();
			foreach (AliquotOrder aliquotOrder in this)
			{
				if (aliquotOrder.IsBlock() == true && aliquotOrder.Printed == false)
				{
					result.Add(aliquotOrder);
				}
			}
			return result;
		}

		public void SetPrinted()
		{
			foreach (AliquotOrder aliquotOrder in this)
			{
				aliquotOrder.Printed = true;
			}
		}

		private List<AliquotOrder> GetPrintableBlocks()
		{
			List<AliquotOrder> result = new List<AliquotOrder>();
			foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in this)
			{
				if (aliquotOrder.IsBlock() == true)
				{
					result.Add(aliquotOrder);
				}
			}
			return result;
		}

		public bool HasUnverifiedBlocks()
		{
			bool result = false;
			List<AliquotOrder> printableBlocks = this.GetPrintableBlocks();
			foreach (AliquotOrder aliquotOrder in printableBlocks)
			{
				if (aliquotOrder.GrossVerified == false)
				{
					result = true;
					break;
				}
			}
			return result;
		}

        public bool HasBlocks()
        {
            bool result = false;            
            foreach (AliquotOrder aliquotOrder in this)
            {
                if (aliquotOrder.AliquotType == "Block")
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasCytycSlide()
        {
            bool result = false;
            foreach (AliquotOrder aliquotOrder in this)
            {
                if (aliquotOrder.AliquotType == "Cytyc Slide")
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasDirectPrintBlocks()
        {
            bool result = false;
            foreach (AliquotOrder aliquotOrder in this)
            {
                if (aliquotOrder.AliquotType == "Block" && aliquotOrder.LabelType == YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint)
                {                    
                    result = true;
                    break;
                }
            }
            return result;
        }

		public bool IsLast(string aliquotOrderId)
		{
			bool result = false;
			List<AliquotOrder> printableBlocks = this.GetPrintableBlocks();
			if (printableBlocks[printableBlocks.Count - 1].AliquotOrderId == aliquotOrderId)
			{
				result = true;
			}
			return result;
		}		

		public bool AliquotTypeExists(string aliquotType)
		{
			bool result = false;
			foreach (AliquotOrder aliquotOrder in this)
			{
				if (aliquotOrder.AliquotType == aliquotType)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public AliquotOrder GetLastBlock()
		{
			AliquotOrder result = null;
			for (int idx = this.Count - 1; idx > -1; idx--)
			{
				if (this[idx].IsBlock() == true)
				{
					result = this[idx];
					break;
				}
			}
			return result;
		}

		public void ValidateBlock(string aliquotOrderId, int validatingUserId)
		{
			foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in this)
			{
				if (aliquotOrder.AliquotOrderId == aliquotOrderId)
				{
					if (aliquotOrder.GrossVerified == false)
					{
						aliquotOrder.GrossVerified = true;
						aliquotOrder.GrossVerifiedById = validatingUserId;
						aliquotOrder.GrossVerifiedDate = DateTime.Now;
						break;
					}
				}
			}
		}

		public bool Exists(string aliquotOrderId)
		{
			bool result = false;
			foreach (AliquotOrder aliquotOrder in this)
			{
				if (aliquotOrder.AliquotOrderId == aliquotOrderId)
				{
					result = true;
					break;
				}
			}
			return result;
		}        

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
        }
	}
}