using AutoMapper;
using MainMarket.Services.ProductAPI.Data;
using MainMarket.Services.ProductAPI.Exceptions;
using MainMarket.Services.ProductAPI.Interfaces;
using MainMarket.Services.ProductAPI.Models.DTO;
using MainMarket.Services.ProductAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainMarket.Services.ProductAPI.Repository;

public class CouponRepository : ICouponRepository
{
    private readonly CouponContext _couponContext;
    private readonly DbSet<Coupon> _dbSet;
    private readonly IMapper _mapper;

    public CouponRepository(CouponContext couponContext, IMapper mapper)
    {
        _couponContext = couponContext;
        _dbSet = _couponContext.Set<Coupon>();
        _mapper = mapper;
    }

    public async Task<CouponDto> CreateCoupon(CouponDto couponDto)
    {
        var couponEntity = _mapper.Map<Coupon>(couponDto);

        couponEntity.CreatedAt = DateTime.UtcNow;
        couponEntity.UpdatedAt = DateTime.UtcNow;

        var response = await _dbSet.AddAsync(couponEntity);
        await _couponContext.SaveChangesAsync();
        return _mapper.Map<CouponDto>(response.Entity);
    }

    public async Task<List<CouponDto>> GetCoupons()
    {
        var coupons = await _dbSet
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<CouponDto>>(coupons);
    }

    public async Task<CouponDto> GetCoupon(string Id)
    {
        var couponEntity = await Task.FromResult(_dbSet.FirstOrDefault(x => x.CouponId == Id) ?? throw new NotFoundException("Coupon Id not found"));
        return _mapper.Map<CouponDto>(couponEntity);
    }

    public async Task<CouponDto> GetCouponByCode(string code)
    {
        var couponEntity = await Task.FromResult(_dbSet.FirstOrDefault(x => x.CouponCode == code) ?? throw new NotFoundException("Coupon code does not exist"));
        return _mapper.Map<CouponDto>(couponEntity);
    }

    public async Task DeleteCoupon(string Id)
    {
        var couponEntity = _dbSet.FirstOrDefault(x => x.CouponId == Id) ?? throw new NotFoundException("Coupon Id not found");
        _dbSet.Remove(couponEntity);
        await _couponContext.SaveChangesAsync();
    }

    public async Task<CouponDto> UpdateCoupon(CouponDto couponDto)
    {
        var couponEntity = _mapper.Map<Coupon>(couponDto);
        couponEntity.UpdatedAt = DateTime.UtcNow;

        var response = _dbSet.Update(couponEntity);
        await _couponContext.SaveChangesAsync();

        return _mapper.Map<CouponDto>(response.Entity);
    }
}