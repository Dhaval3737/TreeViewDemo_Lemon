using LemonTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LemonTest.Controllers
{
    public class NodeController : Controller
    {
        NodeDataContext obj = new NodeDataContext();


        public ActionResult Node(int? NodeId)
        {
            Nodemodel retobj = new Nodemodel();

            retobj.PrentNodeList = obj.getParentNodeList(null).ConvertAll(a =>
            {
                return new SelectListItem() { Text = a.ParentNodeName, Value = Convert.ToString(a.ParentNodeId), Selected = a.ParentNodeId == retobj.ParentNodeId };
            });
            retobj.PrentNodeList.Insert(0, new SelectListItem() { Text = "Select", Value = string.Empty });

            if (NodeId != null && NodeId > 0)
            {
                retobj = obj.getNodeList(NodeId).FirstOrDefault();

                retobj.PrentNodeList = obj.getParentNodeList(null).ConvertAll(a =>
                {
                    return new SelectListItem() { Text = a.ParentNodeName, Value = Convert.ToString(a.ParentNodeId), Selected = a.ParentNodeId == retobj.ParentNodeId };
                });
                retobj.PrentNodeList.Insert(0, new SelectListItem() { Text = "Select", Value = string.Empty});

                return View(retobj);
            }
            else
            {
                return View(retobj);
            }
        }

        [HttpGet]
        public ActionResult NodeList()
        {
            NodeListModel retobj = new NodeListModel();

            retobj.NodeList = obj.getNodeList(null);
            return View(retobj);
        }

        [HttpPost]
        public ActionResult saveNodeDetails(Nodemodel request)
        {
            try
            {
                NodeDataContext obj = new NodeDataContext();
                var data = obj.SaveNode(request);
                if (data == 101)
                {
                    common.setAlertMessage(this, (int)common.MessageTypes.Success, "Node added Successfully");
                    return RedirectToAction("NodeList", "Node");
                }
                else if (data == 102)
                {
                    common.setAlertMessage(this, (int)common.MessageTypes.Success, "Node Updated Successfully.");
                    return RedirectToAction("NodeList", "Node");
                }
                else
                {
                    common.setAlertMessage(this, (int)common.MessageTypes.Success, "Something went wrong while processing your request.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                TempData["ErrorMessage"] = "An error occurred while processing your request. Please try again later.";
                return View();
            }

        }

        [HttpPost]
        public ActionResult deleteNode(int? NodeId)
        {
            int ResultCode = obj.deleteNode(NodeId);
            if (ResultCode == 103)
            {
                common.setAlertMessage(this, (int)common.MessageTypes.Success, "Node Deleted Successfully.");
                return RedirectToAction("NodeList", "Node");
            }
            else
            {
                common.setAlertMessage(this, (int)common.MessageTypes.Error, "Error in Delete.");
                return RedirectToAction("NodeList", "Node");
            }
        }

        public ActionResult TreeView()
        {
            NodeListModel retobj = new NodeListModel();

            retobj.NodeList = obj.getNodeList(null).OrderBy(a => a.NodeName).ToList();
            return View(retobj);
           
        }
    }
}