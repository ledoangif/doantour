<template>
    <HeaderDetail />
    <section class="packages">
        <div class="container py-lg-4">
            <div class="row text-center">
              <h3 class="text-danger">
                <i class="fas fa-star text-warning"></i>
                   Đặt đơn thành công!!
                <i class="fas fa-star text-warning"></i>
               </h3>
            </div>

            <div class="row mt-5">
                <div class="col-7">
                    <div
                        class="border border-secondary form-control border-opacity-50"
                        style="min-height: 270px"
                    >
                        <div class="row container ms-0 border-bottom">
                            <h4 class="text-danger py-3 text-uppercase">
                                Thông tin liên lạc
                            </h4>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <div class="ms-3 py-3">
                                    <span class="text-muted">Họ tên</span><br />
                                    <h5>{{ Customer.nameCustomer }}</h5>
                                </div>
                                <div class="ms-3 py-2">
                                    <span class="text-muted">Địa chỉ</span><br />
                                    <h5>{{ Customer.address }}</h5>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="ms-3 py-3">
                                    <span class="text-muted">Email</span><br />
                                    <h5>{{ Customer.email }}</h5>
                                </div>
                                <div class="ms-3 py-2">
                                    <span class="text-muted">Số điện thoại </span><br />
                                    <h5>{{ maskPhoneNumber(Customer.phoneNumber) }}</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div
                        class="border border-secondary form-control border-opacity-50 mt-4"
                        style="min-height: 520px"
                    >
                        <div class="row container border-bottom ms-0">
                            <h4 class="text-danger py-3 text-uppercase">
                                Chi Tiết Booking
                            </h4>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3"><h6 class="ms-3">Số booking</h6></div>
                            <div class="col-7 d-flex flex-row">
                                <h6 class="text-danger pt-1 me-2">
                                    {{ bookingData.BookingId }}
                                </h6>
                                <span>
                                    (Quý khách vui lòng nhớ số booking để thuận tiện cho
                                    các giao dịch sau này)
                                </span>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3"><h6 class="ms-3">Trị giá booking</h6></div>
                            <div class="col-7 d-flex flex-row">
                                <span>{{
                                   formatCurrency( bookingData.toTalBill)
                                }}</span>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3"><h6 class="ms-3">Số tiền đã trả</h6></div>
                            <div class="col-7 d-flex flex-row">
                                <span>{{
                                     formatCurrency(Booking.paymented)
                                }}</span>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3"><h6 class="ms-3">Số tiền còn lại</h6></div>
                            <div class="col-7 d-flex flex-row">
                                <span>{{
                                      formatCurrency( bookingData.toTalBill)
                                }}</span>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3"><h6 class="ms-3">Ngày đăng ký</h6></div>
                            <div class="col-7 d-flex flex-row">
                                <span>{{ formatDatePayment(Booking.paymentTime) }}</span>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3">
                                <h6 class="ms-3">Hình thức thanh toán</h6>
                            </div>
                            <div class="col-7 d-flex flex-row">
                                <span>{{ Booking.paymentBy }}</span>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3">
                                <h6 class="ms-3">Tình trạng</h6>
                            </div>
                            <div class="col-7 d-flex flex-row">
                                <span
                                    >Booking của quý khách đã được chúng tôi xác nhận
                                    thành công</span
                                >
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3">
                                <h6 class="ms-3">Thời hạn thanh toán</h6>
                            </div>
                            <div class="col-7">
                                <span class="text-danger fw-bold">{{
                                    paymentDeadline(Booking.paymentBy,Booking.paymentTime)
                                }}</span>

                                (Theo giờ Việt Nam. Booking sẽ tự động hủy nếu quá thời
                                hạn thanh toán trên)
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-5">
                    <div
                        class="border border-secondary form-control border-opacity-50"
                        style="height: 420px"
                    >
                        <div class="row container border-bottom ms-0">
                            <h4 class="text-danger py-3 text-uppercase">
                                PHIẾU XÁC NHẬN BOOKING
                            </h4>
                        </div>
                        <div class="d-flex flex-row pb-4 container border-bottom p-0">
                            <div class="col-3 p-0 m-0">
                                <div class="image-tour5 position-relative mt-3 ms-2">
                                    <img
                                        :src="Tour.image"
                                        class="img-fluid"
                                        style="
                                            width: 100%;
                                            height: 100%;
                                            object-fit: cover;
                                        "
                                    />
                                </div>
                            </div>
                            <div class="col-9">
                                <h6 class="mt-3">{{ Tour.nameTour }}</h6>
                                <h6 class="mt-2">
                                    Số booking :
                                    <span class="text-danger">
                                        {{ bookingData.BookingId }}</span
                                    >
                                </h6>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3">
                                <h6 class="ms-3">Mã tour</h6>
                            </div>
                            <div class="col-7 d-flex flex-row justify-content-end">
                                <h6>
                                    {{ bookingData.tourId }}
                                </h6>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3">
                                <h6 class="ms-3">Ngày đi</h6>
                            </div>
                            <div class="col-7 d-flex flex-row justify-content-end">
                                <h6>
                                    {{ formatDate(Tour.dateStart) }}
                                </h6>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-3">
                                <h6 class="ms-3">Ngày về</h6>
                            </div>
                            <div class="col-7 d-flex flex-row justify-content-end">
                                <h6>
                                    {{ formatDate(Tour.dateEnd) }}
                                </h6>
                            </div>
                        </div>
                        <div class="row my-4">
                            <div class="col-4">
                                <h6 class="ms-3">Nơi khởi hành</h6>
                            </div>
                            <div class="col-6 d-flex flex-row justify-content-end">
                                <h6>
                                    {{ Tour.placeStart }}
                                </h6>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div
                class="border border-secondary form-control border-opacity-25 my-5"
                style="min-height: 270px"
            >
                <div class="row border-bottom container ms-0">
                    <h4 class="text-danger py-3 text-uppercase">DANH SÁCH HÀNH KHÁCH</h4>
                </div>
                <table class="table">
                    <thead class="table-secondary">
                        <tr>
                            <th scope="col">Họ tên</th>
                            <th scope="col">Ngày sinh</th>
                            <th scope="col">Độ tuổi</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="customer in customers">
                            <td>{{ customer.name }}</td>
                            <td>{{ customer.dateOfBirth }}</td>
                            <td v-if="customer.age > 12">Người lớn</td>
                            <td v-else>Trẻ em</td>
                        </tr>
                    </tbody>
                </table>
                <div class="d-flex flex-row justify-content-end">
                    <h5 class="me-2 text-danger">Tổng tiền :</h5>
                    <h5 class="text-danger">
                        {{
                            formatCurrency( bookingData.toTalBill)
                        }}
                    </h5>
                </div>
            </div>
        </div>
    </section>
    <div class="text-center m-3">
        <NuxtLink to="/" class="btn btn-warning p-3">Về trang chủ</NuxtLink>
    </div>
    <Footer />
</template>
<script setup>
import Api from '~/service/Base/api.ts';
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { formatDate, formatDatePayment, formatCurrency, paymentDeadline, maskPhoneNumber } from '~/assets/js/validate';
import { Base64 } from 'js-base64';
/**
 * initialization
 */
 const api = new Api();
const Tour = ref([]);
const Customer = ref({});
const Booking = ref({});
const customers = ref([]);
const route = useRoute();
const router = useRouter();
const bookingData = ref({});

onMounted(async () => {
    const encodedData = route.query.data;
    if (encodedData) {
        try {
            bookingData.value = JSON.parse(Base64.decode(encodedData));
            const { customerId, tourId, BookingId, listCustomer } = bookingData.value;

            const getTour = await api.get(`/Tour/${tourId}`);
            Tour.value = getTour.data.responseData;

            const getCustomer = await api.get(`/Customer/${customerId}`);
            Customer.value = getCustomer.data.responseData;

            const getBooking = await api.get(`/Booking/${BookingId}`);
            Booking.value = getBooking.data.responseData;

            customers.value = handleListCustomer(listCustomer);
        } catch (error) {
            console.error('Failed to decode booking data:', error);
            router.push('/error'); // Redirect to an error page or show an error message
        }
    } else {
        console.error('No booking data found in query parameters');
        router.push('/error'); // Redirect to an error page or show an error message
    }
});

const handleListCustomer = (htmlString) => {
    const parser = new DOMParser();
    const doc = parser.parseFromString(htmlString, 'text/html');
    const spans = doc.querySelectorAll('p');
    const customers = [];

    spans.forEach((span) => {
        const text = span.textContent;
        const [fullName, dob] = text.split(',').map((item) => item.trim());
        const [day, month, year] = dob.split('/').map((item) => parseInt(item.trim(), 10));
        const age = calculateAge(new Date(year, month - 1, day));
        customers.push({ name: fullName, dateOfBirth: dob, age });
    });

    return customers;
};

const calculateAge = (birthday) => {
    const ageDate = new Date(Date.now() - birthday.getTime());
    return Math.abs(ageDate.getUTCFullYear() - 1970);
};

definePageMeta({
    layout: false,
});
</script>

<style scoped>
@import '../../assets/css/bootstrap.css';
@import '../../assets/css/style.css';
@import '../../assets/css/font-awesome.min.css';
@import '../../assets/css/css_slider.css';
@import '../../assets/vendor/mdi-font/css/material-design-iconic-font.min.css';
@import '../../assets/vendor/font-awesome-4.7/css/font-awesome.min.css';
@import '../../assets/vendor/select2/select2.min.css';

@keyframes sparkle {
    0%, 100% {
        transform: scale(1);
        opacity: 1;
    }
    50% {
        transform: scale(1.5);
        opacity: 0.7;
    }
}

.text-center h3 .fa-star {
    display: inline-block;
    margin: 0 10px;
    animation: sparkle 1s infinite;
}

.text-center h3 .fa-star:first-child {
    animation-delay: 0.5s;
}
</style>