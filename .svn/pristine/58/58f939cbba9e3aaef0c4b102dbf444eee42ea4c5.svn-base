/************************该页面为预约登记列表页公用脚本******************************/
// 记录读取分页数据操作的次数，用于判断是否进行回调
// 1、只有第1次才调用 jQuery("#Pagination").pagination
// 2、只有第2次及以后的操作才调用回调函数 pageselectCallback 中的 QueryPagesData(page_index );

var tempOperPageCount = 0;
var tempOldtotalCount = 0; //初始总页数，用于判断是否更新页码
var editTitle = "点击进行修改"; //编辑提示
function pageselectCallback(page_index, jq) {

    if (tempOperPageCount > 0) {
        QueryPagesData(page_index);
    }
    tempOperPageCount++;

    return false;
}

jQuery(document).ready(function () {

    QueryPagesData(0);

});

function QueryPagesData(pageIndex) {
    optInit = getOptionsFromForm();
    var modelName = jQuery("#modelName").val();
    var totalCount = 0;
    var CustomerName = jQuery.trim(jQuery('#txtSFZ').val());
    var optInit;
    jQuery.ajax({
        type: "GET",
        url: "/Ajax/AjaxRegiste.aspx",
        data: { modelName: modelName, pageIndex: pageIndex, CustomerName: CustomerName, pageSize: pagePagination.items_per_page, action: 'GetRegisteCustomerList' },
        cache: false,
        dataType: "json",
        success: function (msg) {
            if (parseInt(msg.totalCount) > 0) {
                jQuery("#Pagination").show();
                if (tempOperPageCount == 0) {
                    jQuery("#Pagination").pagination(msg.totalCount, optInit);
                    tempOldtotalCount = msg.totalCount;
                    tempOperPageCount = 1;
                }
                else if (tempOldtotalCount != msg.totalCount) {
                    jQuery("#Pagination").pagination(msg.totalCount, optInit);
                    tempOperPageCount = 1;
                }


                var newcontent = '';
                var templateContent = '<tr id="@ID_ArcCustomer"><td><input ArcCustomer="@ID_ArcCustomer" id="@ID_Customer" title="" type="checkbox" name="ItemCheckbox"/></td>' +
                        '<td><a  href="javascript:void(0);" onclick="DoLoadX(this);" targeturl="/System/Customer/RegistOper.aspx?type=Edit&modelName=@modelName&ID_ArcCustomer=@ID_ArcCustomer&ID_Customer=@ID_Customer" title="@editTitle">@ID_Customer</a></td>' +
                        '<td><lable>@CustomerName</lable></td>' +
                        '<td>@GenderName</td>' +
                        '<td>@age</td>' +
                        '<td>@IDCard</td>' +
                        '<td>@MarriageName</td>' +
                        '<td>@MobileNo</td>' +
                        '<td>@Is_Team</td>' +
                        '<td>@Is_FeeSettled</td>' +
                        '<td>@Is_Subscribed</td>' +
                        '<td><a href="javascript:void(0);" onclick="DoLoadX(this);" targeturl="/System/Customer/RegistOper.aspx?type=Edit&modelName=@modelName&ID_ArcCustomer=@ID_ArcCustomer&ID_Customer=@ID_Customer" title="@editTitle">编辑</a></td>' +
                        '</tr>';
                jQuery(msg.dataList).each(function (i, item) {
                    
                    newcontent += templateContent.replace(/@ID_Customer/gi, item.ID_Customer)
                            .replace(/@ID_ArcCustomer/gi, item.ID_ArcCustomer)
                            .replace(/@CustomerName/gi, item.CustomerName)
                            .replace(/@GenderName/gi, item.GenderName)
                            .replace(/@IDCard/gi, item.IDCard)
                            .replace(/@MarriageName/gi, item.MarriageName)
                            .replace(/@MobileNo/gi, item.MobileNo)
                            .replace(/@Is_Team/gi, item.Is_Team)
                            .replace(/@Is_FeeSettled/gi, item.Is_FeeSettled)
                            .replace(/@Is_Subscribed/gi, item.Is_Subscribed)
                            .replace(/@modelName/gi, modelName)
                            .replace(/@editTitle/gi, editTitle)
                            .replace(/@age/gi, item.age)
                            ;
                });
                if (newcontent != '') {
                    jQuery('#Searchresult').html(newcontent);
                }
            } else {
                jQuery('#Searchresult').html("");
                jQuery("#Pagination").hide();
                //jQuery("#Pagination").pagination(0, optInit);
            }
        }
    });
}
function DoLoadX(obj) {
    if (jQuery(obj).attr("targeturl") != "") {
        DoLoad(jQuery(obj).attr("targeturl"), '');
    }
}

/*删除 Begin*/
function DoDel() {
    if (confirm("您确认要删除吗？")) {
        //判断是否有选中项目
        var ItemExamCard = '';
        var ItemArcCustomer = '';
        jQuery("[name='ItemCheckbox']").each(function () {
            if (jQuery(this).attr('checked')) {
                jQuery(this).parent().parent().remove();
                ItemExamCard += "'" + jQuery(this).attr("id") + "',";
                ItemArcCustomer += "'" + jQuery(this).attr("ArcCustomer") + "',";
            }
        });
        var qustData = { action: 'DelData',
            type: "Registe",
            ItemExamCard: ItemExamCard,
            ItemArcCustomer: ItemArcCustomer
        };
        if (ItemExamCard != '') {
            //存储大数据请设置Content-length值
            jQuery.ajax({
                type: "POST",
                url: "/Ajax/AjaxRegiste.aspx",
                data: qustData,
                cache: false,
                contentType: "application/x-www-form-urlencoded;Content-length=1024000",
                dataType: "json",
                success: function (msg) {
                    ShowSystemDialog(msg.Message);
                    if (msg.success == "1") {
                        Search();
                    }
                    else {

                    }

                }
            });
        }
    }
    else {
        return false;
    }
}
/*删除 End*/

function Search() {

    tempOperPageCount = 0;
    QueryPagesData(0); //重新按照新输入的条件进行查询

}