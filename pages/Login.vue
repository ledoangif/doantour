 <template>
    <div class="login min-vh-100 min-vw-100">
        <div class="main pt-5">
            <div class="text-center p-5">
                <img
                    src="../assets/images/logo4.png"
                    style="width: 15%; border-radius: 5%"
                    alt="logo"
                />
            </div>

            <div class="bg-light p-4 rounded shadow mx-auto" style="width: 25%;">
                <div class="fs-3 text-center mb-4 text-dark">ĐĂNG NHẬP TÀI KHOẢN</div>

                <form @submit.prevent="login">
                    <div class="mt-10">
                        <div class="col-lg-12 mx-auto">
                            <div class="input-group mb-3">
                                <input
                                    type="text"
                                    class="form-control"
                                    aria-label="Sizing example input"
                                    v-model="User.email"
                                    name="email"
                                    placeholder="Email"
                                    aria-describedby="inputGroup-sizing-lg"
                                    @blur="validateEmail"
                                />
                            </div>
                            <div v-if="emailError" class="text-danger">{{ emailError }}</div>

                            <div class="input-group mb-3">
                                <input
                                    type="password"
                                    class="form-control"
                                    aria-label="Sizing example input"
                                    placeholder="Password"
                                    v-model="User.password"
                                    aria-describedby="inputGroup-sizing-lg"
                                    @blur="validatePassword"
                                />
                            </div>
                            <div v-if="passwordError" class="text-danger">{{ passwordError }}</div>
                        </div>
                    </div>
                    <div class="text-center m-4">
                        <button type="submit" class="btn btn-primary">Đăng Nhập</button>
                    </div>
                </form>
                <!-- Thông báo lỗi đăng nhập sẽ được hiển thị ở đây -->
                <div v-if="errorMessage" class="text-danger text-center fs-5">{{ errorMessage }}</div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import Api from '~/service/Base/api.ts';

/** auth */
definePageMeta({
    layout: false,
});

/**init data */
const User = ref({
    email: '',
    password: '',
});
const errorMessage = ref('');
const emailError = ref(null);
const passwordError = ref(null);
const api = new Api();

/**
 * Validation logic
 */
const validateEmail = () => {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!User.value.email) {
        emailError.value = 'Email không được để trống';
    } else if (!emailPattern.test(User.value.email)) {
        emailError.value = 'Vui lòng nhập email hợp lệ';
    } else {
        emailError.value = null;
    }
};

const validatePassword = () => {
    if (!User.value.password) {
        passwordError.value = 'Mật khẩu không được để trống';
    } else if (User.value.password.length < 5) {
        passwordError.value = 'Mật khẩu phải có ít nhất 5 ký tự!';
    } else if (!/(?=.*[a-z])(?=.*[A-Z])(?=.*\d)/.test(User.value.password)) {
        passwordError.value = 'Mật khẩu phải có ít nhất 5 ký tự gồm chữ hoa, chữ thường và số.';
    } else {
        passwordError.value = null;
    }
};

/**
 * Handle login
 */
const login = async () => {
    // Validate before submitting
    validateEmail();
    validatePassword();

    if (emailError.value || passwordError.value) {
        return;
    }

    try {
        const formData = new FormData();
        formData.append('email', User.value.email);
        formData.append('password', User.value.password);

        const response = await api.postAPI('/Account/login', formData);

        // Save login info to localStorage
        localStorage.setItem('token', response.data.responseData.accessToken);
        localStorage.setItem('userId', response.data.responseData.userId);
        localStorage.setItem('userFullName', response.data.responseData.userFullName);
        localStorage.setItem('email', response.data.responseData.email);
        localStorage.setItem('role', response.data.responseData.role);

        navigateTo('/booking');
    } catch (error) {
        console.log(error);
        // Kiểm tra lỗi từ server và hiển thị thông báo tương ứng
        if (error.response && error.response.data && error.response.data.message) {
            errorMessage.value = error.response.data.message;
        } else {
            errorMessage.value = 'Tên đăng nhập hoặc mật khẩu không đúng';
        }
    }
};
</script>

<style scoped>
.bg-light {
    background-color: rgba(255, 255, 255, 0.9);
}

.shadow {
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.text-danger {
    color: #dc3545 !important; /* Màu đỏ đậm cho thông báo lỗi */
}
</style>
