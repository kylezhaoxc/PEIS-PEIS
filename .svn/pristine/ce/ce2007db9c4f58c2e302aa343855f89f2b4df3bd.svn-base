/**
 * This jQuery plugin displays pagination links inside the selected elements.
 *
 * @author Gabriel Birke (birke *at* d-scribe *dot* de)
 * @version 1.2
 * @param {int} maxentries Number of entries to paginate
 * @param {Object} opts Several options (see README for documentation)
 * @return {Object} jQuery Object
 */
jQuery.fn.pagination = function (maxentries, opts) {
    opts = jQuery.extend({
        items_per_page: 10,
        num_display_entries: 10,
        current_page: 0,
        num_edge_entries: 0,
        link_to: "#",
        prev_text: "Prev",
        next_text: "Next",
        style: "default",
        ellipse_text: "...",
        totalnumber_prev_text: "Total:",
        totalnumber_next_text: "",
        prev_show_always: true,
        next_show_always: true,
        callback: function () { return false; }
    }, opts || {});

    return this.each(function () {
        /**
        * Calculate the maximum number of pages
        */
        function numPages() {
            return Math.ceil(maxentries / opts.items_per_page);
        }

        /**
        * Calculate start and end point of pagination links depending on 
        * current_page and num_display_entries.
        * @return {Array}
        */
        function getInterval() {
            var ne_half = Math.ceil(opts.num_display_entries / 2);
            var np = numPages();
            var upper_limit = np - opts.num_display_entries;
            var start = current_page > ne_half ? Math.max(Math.min(current_page - ne_half, upper_limit), 0) : 0;
            var end = current_page > ne_half ? Math.min(current_page + ne_half, np) : Math.min(opts.num_display_entries, np);
            return [start, end];
        }

        /**
        * This is the event handling function for the pagination links. 
        * @param {int} page_id The new page number
        */
        function pageSelected(page_id, evt) {
            current_page = page_id;
            drawLinks();
            var continuePropagation = opts.callback(page_id, panel);
            if (evt && !continuePropagation) {
                if (evt.stopPropagation) {
                    evt.stopPropagation();
                }
                else {
                    evt.cancelBubble = true;
                }
            }
            return continuePropagation;
        }

        /**
        * This function inserts the pagination links into the container element
        */
        function drawLinks() {
            panel.empty();
            var interval = getInterval();
            var np = numPages();
            // This helper function returns a handler function that calls pageSelected with the right page_id
            var getClickHandler = function (page_id) {
                return function (evt) { return pageSelected(page_id, evt); }
            }
            if (opts.style == "default") {
                var lnk = "";

                if (current_page > 0) {
                    // 显示 "第一页"- 有 Link
                    lnk = jQuery("<li><a href='javascript:void(0);' class='paging-bar-bg'><span class='paging-bar-zq'></span></a></li>")
						.bind("click", getClickHandler(0))
						.attr('href', opts.link_to.replace(/__id__/, 0));
                } else {
                    // 显示 "第一页"- 无 Link
                    lnk = jQuery("<li><a href='javascript:void(0);' class='paging-bar-bg'><span class='paging-bar-zq-no'></span></a></li>");
                }
                panel.append(lnk);

                if (current_page > 0) {
                    // 显示 "上一页"- 有 Link
                    lnk = jQuery("<li title=" + opts.prev_text + "><a href='javascript:void(0);' class='paging-bar-bg'><span class='paging-bar-sy'></span></a></li>")
						.bind("click", getClickHandler(current_page - 1))
						.attr('href', opts.link_to.replace(/__id__/, current_page - 1));
                } else {
                    // 显示 "上一页"- 无 Link 
                    lnk = jQuery("<li title=" + opts.prev_text + "><a href='javascript:void(0);' class='paging-bar-bg'><span class='paging-bar-sy-no'></span></a></li>");
                }
                panel.append(lnk);

                lnk = jQuery("<li><span class='paging-bar-fenge'></span></li>");
                panel.append(lnk);
                lnk = jQuery("<li><span class='paging-bar-input'><input name='inputnumber' totalpages='" + np + "' maxlength='4' type='text' value='" + (current_page + 1) + "' /></span></li>");
                lnk.find('input')
                .bind("keyup.pager", function (e) {

                    this.value = this.value.replace(/\D/g, ''); // 限制只能输入数字
                    if (parseFloat(this.value) > parseFloat(np)) {
                        this.value = np;
                    }
                    if (e.keyCode == 13) {
                        pageSelected(~ ~this.value - 1, e);
                    }
                })
                .bind("afterpaste", function (e) {

                    this.value = this.value.replace(/\D/g, ''); // 限制只能输入数字
                    if (parseFloat(this.value) > parseFloat(np)) {
                        this.value = np;
                    }
                })
                .blur(function () {
                    pageSelected(~ ~this.value - 1);
                });
                panel.append(lnk);
                lnk = jQuery("<li><span class='paging-bar-fens'>/" + np + "</span></li>");
                panel.append(lnk);
                lnk = jQuery("<li><span class='paging-bar-fenge'></span></li>");
                panel.append(lnk);

                if (current_page < np - 1) {
                    // 显示 "下一页"- 有 Link
                    lnk = jQuery("<li title=" + opts.next_text + "><a href='javascript:void(0);' class='paging-bar-bg'><span class='paging-bar-xy'></span></a></li>")
                        .bind("click", getClickHandler(current_page + 1))
						.attr('href', opts.link_to.replace(/__id__/, current_page + 1));
                }
                else {
                    // 显示 "下一页"- 无 Link
                    lnk = jQuery("<li><a href='javascript:void(0);' class='paging-bar-bg'><span class='paging-bar-xy-no'></span></a></li>");
                }
                panel.append(lnk);

                if (current_page < np - 1) {
                    lnk = jQuery("<li><a href='javascript:void(0);' class='paging-bar-bg'><span class='paging-bar-zh'></span></a></li>")
                        .bind("click", getClickHandler(np - 1))
						.attr('href', opts.link_to.replace(/__id__/, np - 1));
                } else {
                    lnk = jQuery("<li><a href='javascript:void(0);' class='paging-bar-bg'><span class='paging-bar-zh-no'></span></a></li>");
                }
                panel.append(lnk);


                lnk = jQuery("<li><span class='paging-bar-goji'>" + opts.totalnumber_prev_text + maxentries + opts.totalnumber_next_text + "</span></li>");
                panel.append(lnk);

            }
            else if (opts.style == "numberlist") {
                // Helper function for generating a single link (or a span tag if it's the current page)
                var appendItem = function (page_id, appendopts) {
                    page_id = page_id < 0 ? 0 : (page_id < np ? page_id : np - 1); // Normalize page id to sane value
                    appendopts = jQuery.extend({ text: page_id + 1, classes: "" }, appendopts || {});
                    if (page_id == current_page) {
                        var lnk = jQuery("<li class='current'><a href='javascript:void(0);'>" + (appendopts.text) + "</a></li>");
                    }
                    else {
                        var lnk = jQuery("<li><a href='javascript:void(0);'>" + (appendopts.text) + "</a></li>")
						.bind("click", getClickHandler(page_id))
						.attr('href', opts.link_to.replace(/__id__/, page_id));
                    }
                    if (appendopts.classes) { lnk.addClass(appendopts.classes); }
                    panel.append(lnk);
                }
                // Generate "Previous"-Link
                if (opts.prev_text && (current_page > 0 || opts.prev_show_always)) {
                    appendItem(current_page - 1, { text: opts.prev_text, classes: "prev" });
                }
                // Generate starting points
                if (interval[0] > 0 && opts.num_edge_entries > 0) {
                    var end = Math.min(opts.num_edge_entries, interval[0]);
                    for (var i = 0; i < end; i++) {
                        appendItem(i);
                    }
                    if (opts.num_edge_entries < interval[0] && opts.ellipse_text) {
                        jQuery("<li><a href='javascript:void(0);'>" + opts.ellipse_text + "</a></li>").appendTo(panel);
                    }
                }
                // Generate interval links
                for (var i = interval[0]; i < interval[1]; i++) {
                    appendItem(i);
                }
                // Generate ending points
                if (interval[1] < np && opts.num_edge_entries > 0) {
                    if (np - opts.num_edge_entries > interval[1] && opts.ellipse_text) {
                        jQuery("<li><a href='javascript:void(0);'>" + opts.ellipse_text + "</a></li>").appendTo(panel);
                    }
                    var begin = Math.max(np - opts.num_edge_entries, interval[1]);
                    for (var i = begin; i < np; i++) {
                        appendItem(i);
                    }

                }
                // Generate "Next"-Link
                if (opts.next_text && (current_page < np - 1 || opts.next_show_always)) {
                    appendItem(current_page + 1, { text: opts.next_text, classes: "next" });
                }
                if (maxentries >= 0) {
                    // Generate "Total"-Link
                    appendItem(0, { text: opts.totalnumber_prev_text + maxentries + opts.totalnumber_next_text, classes: "next" });
                }
            }
        }

        // Extract current_page from options
        var current_page = opts.current_page;
        // Create a sane value for maxentries and items_per_page
        maxentries = (!maxentries || maxentries < 0) ? 1 : maxentries;
        opts.items_per_page = (!opts.items_per_page || opts.items_per_page < 0) ? 1 : opts.items_per_page;
        // Store DOM element for easy access from all inner functions
        var panel = jQuery(this);
        // Attach control functions to the DOM element 
        this.selectPage = function (page_id) { pageSelected(page_id); }
        this.prevPage = function () {
            if (current_page > 0) {
                pageSelected(current_page - 1);
                return true;
            }
            else {
                return false;
            }
        }
        this.nextPage = function () {
            if (current_page < numPages() - 1) {
                pageSelected(current_page + 1);
                return true;
            }
            else {
                return false;
            }
        }
        // When all initialisation is done, draw the links
        drawLinks();
        // call callback function
        opts.callback(current_page, this);
    });
}


