function hienthidanhmucluachon(){
    $(".header__category").removeClass("undisplay");
    $(".header__category").addClass("display");
}
function khonghienthidanhmucluachon(){
    $(".header__category").removeClass("display");
    $(".header__category").addClass("undisplay");
}
// slide show

// go to top
var showGoToTop = 300;

$(window).scroll(function(){
    if($(this).scrollTop() >= showGoToTop){
        $('#go-to-top').fadeIn();
    } else {
        $('#go-to-top').fadeOut();
    }
});
$('#go-to-top').click(function(){
    $('html, body').animate({scrollTop: 0 }, 'slow');
});
// change img
function changeImg(index){
    // Tạo ID dựa trên số thứ tự truyền vào (ví dụ: thumb-0, thumb-1)
    let targetId = 'thumb-' + index;

    // Lấy phần tử ảnh nhỏ
    let smallImg = document.getElementById(targetId);

    if (smallImg) {
        // Lấy đường dẫn ảnh
        let imgPath = smallImg.getAttribute('src');

        // Cập nhật cho ảnh chính
        let mainImg = document.getElementById('img-main');
        if (mainImg) {
            mainImg.setAttribute('src', imgPath);

            // Bonus: Thêm hiệu ứng highlight cho ảnh đang chọn
            // Xóa viền đỏ của tất cả ảnh nhỏ trước
            $('.small-img').css('border', '1px solid #000');
            // Thêm viền đỏ cho ảnh vừa click
            $(smallImg).css('border', '2px solid #d9121f');
        }
    }
}
function check(){
    
    var type = document.getElementsByName("mau");
    if(type[0].checked)
    {
        var val = type[0].value;
        console.log(val);
    }
    else if(type[1].checked)
    {
        var val = type[1].value;
        console.log(val);
    }
    else if(type[2].checked)
    {
        var val = type[2].value;
        console.log(val);
    }
    
}
$(document).ready(function(){
    $(window).resize(function() {
        if($(window).width() < 739) {
            $('.collapse').removeClass('show');
        }
        else
        {
            $('.collapse').addClass('show');
        }
    });
    // click mega menu
    $('.header_nav-list .header_nav-list-item a').click(function() {
        $('.header_nav-list-item a').removeClass('active');
        $(this).addClass('active');
    });
    $('.ng-has-child1 > a i').click(function(e){
        e.preventDefault();
        $('.ul-has-child1').toggle('slow');
        $('.cong').toggleClass('hidden');
        $('.tru').toggleClass('hidden');
    })
   
    $('.ng-has-child2 > a i').click(function(e){
        e.preventDefault();
    })
    $('#trigger-mobile').click(function(e){
        $('.mobile-main-menu').toggleClass('xyz');
        $('.overlay').toggleClass('hidden');
    })
    $('.overlay').click(function(e){
        $('.mobile-main-menu').toggleClass('xyz');
        $('.overlay').toggleClass('hidden');
    })
    // click thông tin đơn hàng trang pay
    $('.summary').click(function(){
        $('.summary-content').toggle('slow');
    })
})
function hienthi(id, name){
    $(`#${name}`).toggle('slow');
    $(`.cong${id}`).toggleClass('hidden');
    $(`.tru${id}`).toggleClass('hidden');
}

function updateCartApi(id, sl, type) {
    var requestData = {
        id: parseInt(id),
        sl: parseInt(sl),
        type: type
    };
    $.ajax({


        url: 'https://localhost:7218/api/Buy/UpdateQuantity', 
        type: 'POST',
        accept: '*/*',
        contentType: 'application/json',
        data: JSON.stringify(requestData),
        success: function (response) {
            if (response.success) {
                // Cập nhật lại số lượng trên ô input
                $('#text_so_luong-' + id).val(response.newQty);
                // Cập nhật lại thành tiền của dòng đó (nếu có)
                $('#line-total-' + id).text(response.newLineTotal + '₫');
                // Cập nhật tổng tiền cả giỏ hàng (nếu có)
                $('#grand-total').text(response.newGrandTotal + '₫');
            } else {
                alert(response.message);
                // Nếu lỗi (vượt quá tồn kho), reset lại giá trị cũ từ Server trả về
                $('#text_so_luong-' + id).val(response.currentQty);
            }
        }
    });
}
function cong(id) {
    updateCartApi(id, 0, "+");
}
function tru(id) {
    var value = $('#text_so_luong-' + id).val();
    if(parseInt(value) > 1)
    {
        updateCartApi(id, 0, "-");
    }
    
}
function nhapSo(id, element) {
    var value = parseInt(element.value);
    if (isNaN(value) || value < 1) {
        alert("Số lượng phải lớn hơn 0");
        return;
    }
    updateCartApi(id, value, "=");
}

    function validate(evt) {
  var theEvent = evt || window.event;

  // Handle paste
  if (theEvent.type === 'paste') {
      key = event.clipboardData.getData('text/plain');
  } else {
  // Handle key press
      var key = theEvent.keyCode || theEvent.which;
      key = String.fromCharCode(key);
  }
  var regex = /[0-9]|\./;
  if( !regex.test(key) ) {
    theEvent.returnValue = false;
    if(theEvent.preventDefault) theEvent.preventDefault();
  }
}

function xoa(id) {
    if (confirm("Bạn có chắc muốn bỏ sản phẩm này khỏi giỏ hàng không?")) {
        $.ajax({
            // URL có kèm ID
            url: 'https://localhost:7218/api/Buy/DeleteCartItem/' + id,
            type: 'DELETE', // Phải khớp với [HttpDelete] ở Controller
            success: function (response) {
                if (response.success) {
                    // Xóa dòng đó trên giao diện
                    $(`.cart-body-row-${id}`).fadeOut();

                    // Cập nhật lại tổng tiền giỏ hàng (nếu API trả về tổng mới)
                    $('#grand-total').text(response.newGrandTotal + '₫');

                    console.log("Xóa thành công!");
                } else {
                    alert("Lỗi: " + response.message);
                }
            },
            error: function () {
                alert("Lỗi Ajax khi xóa rồi!");
            }
        });
    }
}