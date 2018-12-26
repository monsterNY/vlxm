/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 80013
Source Host           : localhost:3306
Source Database       : article

Target Server Type    : MYSQL
Target Server Version : 80013
File Encoding         : 65001

Date: 2018-12-26 17:11:01
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for article_info
-- ----------------------------
DROP TABLE IF EXISTS `article_info`;
CREATE TABLE `article_info` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) DEFAULT '0' COMMENT '标题',
  `author` varchar(50) DEFAULT '0' COMMENT '作者',
  `status` int(11) DEFAULT '0' COMMENT '状态码',
  `articleType` bigint(20) NOT NULL DEFAULT '0' COMMENT '文章类型',
  `publishTime` datetime DEFAULT NULL COMMENT '发布时间',
  `createTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `updateTime` datetime DEFAULT NULL,
  `validFlag` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=96 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='文章信息';

-- ----------------------------
-- Records of article_info
-- ----------------------------
INSERT INTO `article_info` VALUES ('1', '测试', 'monster', '0', '0', '2018-12-24 15:40:45', '2018-12-24 15:40:46', '2018-12-24 15:40:46', '1');
INSERT INTO `article_info` VALUES ('2', '测试', 'monster', '0', '0', '2018-12-24 15:40:45', '2018-12-24 15:40:46', '2018-12-24 15:40:46', '1');
INSERT INTO `article_info` VALUES ('3', '测试', 'monster', '0', '0', '2018-12-24 15:40:45', '2018-12-24 15:40:46', '2018-12-24 15:40:46', '1');
INSERT INTO `article_info` VALUES ('4', '测试', 'monster', '0', '0', '2018-12-24 15:40:45', '2018-12-24 15:40:46', '2018-12-24 15:40:46', '1');
INSERT INTO `article_info` VALUES ('5', '测试', 'monster', '0', '0', '2018-12-24 15:40:45', '2018-12-24 15:40:46', '2018-12-24 15:40:46', '1');
INSERT INTO `article_info` VALUES ('6', '测试', 'monster', '0', '0', '2018-12-24 15:40:45', '2018-12-24 15:40:46', '2018-12-24 15:40:46', '1');
INSERT INTO `article_info` VALUES ('7', '测试', 'monster', '0', '0', '2018-12-24 15:40:45', '2018-12-24 15:40:46', '2018-12-24 15:40:46', '1');
INSERT INTO `article_info` VALUES ('8', 'test', 'author', '0', '0', null, '2018-12-26 11:14:09', null, '0');
INSERT INTO `article_info` VALUES ('9', 'test', 'author', '0', '0', null, '2018-12-26 11:14:10', null, '0');
INSERT INTO `article_info` VALUES ('10', 'test', 'author', '0', '0', null, '2018-12-26 11:14:11', null, '0');
INSERT INTO `article_info` VALUES ('11', 'test', 'author', '0', '0', null, '2018-12-26 11:14:12', null, '0');
INSERT INTO `article_info` VALUES ('12', 'test', 'author', '0', '0', null, '2018-12-26 11:14:12', null, '0');
INSERT INTO `article_info` VALUES ('13', 'test', 'author', '0', '0', null, '2018-12-26 11:14:13', null, '0');
INSERT INTO `article_info` VALUES ('14', 'test', 'author', '0', '0', null, '2018-12-26 11:14:14', null, '0');
INSERT INTO `article_info` VALUES ('15', 'test', 'author', '0', '0', null, '2018-12-26 11:14:14', null, '0');
INSERT INTO `article_info` VALUES ('16', 'test', 'author', '0', '0', null, '2018-12-26 11:14:15', null, '0');
INSERT INTO `article_info` VALUES ('17', 'test', 'author', '0', '0', null, '2018-12-26 11:19:29', null, '1');
INSERT INTO `article_info` VALUES ('18', 'test', 'author', '0', '0', null, '2018-12-26 11:19:29', null, '1');
INSERT INTO `article_info` VALUES ('19', 'test', 'author', '0', '0', null, '2018-12-26 11:19:29', null, '1');
INSERT INTO `article_info` VALUES ('20', 'test', 'author', '0', '0', null, '2018-12-26 11:19:29', null, '1');
INSERT INTO `article_info` VALUES ('21', 'test', 'author', '0', '0', null, '2018-12-26 11:19:29', null, '1');
INSERT INTO `article_info` VALUES ('22', 'test', 'author', '0', '0', null, '2018-12-26 11:19:29', null, '1');
INSERT INTO `article_info` VALUES ('23', 'test', 'author', '0', '0', null, '2018-12-26 11:19:29', null, '1');
INSERT INTO `article_info` VALUES ('24', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('25', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('26', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('27', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('28', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('29', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('30', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('31', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('32', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('33', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('34', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('35', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('36', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('37', 'test', 'author', '0', '0', null, '2018-12-26 11:21:53', null, '1');
INSERT INTO `article_info` VALUES ('38', 'test', 'author', '0', '0', null, '2018-12-26 11:21:54', null, '1');
INSERT INTO `article_info` VALUES ('39', 'test', 'author', '0', '0', null, '2018-12-26 11:21:54', null, '1');
INSERT INTO `article_info` VALUES ('40', 'test', 'author', '0', '0', null, '2018-12-26 11:21:54', null, '1');
INSERT INTO `article_info` VALUES ('41', 'test', 'author', '0', '0', null, '2018-12-26 11:21:54', null, '1');
INSERT INTO `article_info` VALUES ('42', 'test', 'author', '0', '0', null, '2018-12-26 11:21:54', null, '1');
INSERT INTO `article_info` VALUES ('43', 'test', 'author', '0', '0', null, '2018-12-26 11:21:54', null, '1');
INSERT INTO `article_info` VALUES ('44', 'test', 'author', '0', '0', null, '2018-12-26 11:21:54', null, '1');
INSERT INTO `article_info` VALUES ('45', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('46', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('47', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('48', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('49', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('50', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('51', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('52', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('53', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('54', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('55', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('56', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('57', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('58', 'test', 'author', '0', '0', null, '2018-12-26 11:21:55', null, '1');
INSERT INTO `article_info` VALUES ('59', 'test', 'author', '0', '0', null, '2018-12-26 11:21:56', null, '1');
INSERT INTO `article_info` VALUES ('60', 'test', 'author', '0', '0', null, '2018-12-26 11:21:56', null, '1');
INSERT INTO `article_info` VALUES ('61', 'test', 'author', '0', '0', null, '2018-12-26 11:21:56', null, '1');
INSERT INTO `article_info` VALUES ('62', 'test', 'author', '0', '0', null, '2018-12-26 11:21:56', null, '1');
INSERT INTO `article_info` VALUES ('63', 'test', 'author', '0', '0', null, '2018-12-26 11:21:56', null, '1');
INSERT INTO `article_info` VALUES ('64', 'test', 'author', '0', '0', null, '2018-12-26 11:21:56', null, '1');
INSERT INTO `article_info` VALUES ('65', 'test', 'author', '0', '0', null, '2018-12-26 11:21:56', null, '1');
INSERT INTO `article_info` VALUES ('66', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('67', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('68', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('69', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('70', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('71', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('72', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('73', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('74', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('75', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('76', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('77', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('78', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('79', 'test', 'author', '0', '0', null, '2018-12-26 11:21:57', null, '1');
INSERT INTO `article_info` VALUES ('80', 'test', 'author', '0', '0', null, '2018-12-26 11:21:58', null, '1');
INSERT INTO `article_info` VALUES ('81', 'test', 'author', '0', '0', null, '2018-12-26 11:21:58', null, '1');
INSERT INTO `article_info` VALUES ('82', 'test', 'author', '0', '0', null, '2018-12-26 11:21:58', null, '1');
INSERT INTO `article_info` VALUES ('83', 'test', 'author', '0', '0', null, '2018-12-26 11:21:58', null, '1');
INSERT INTO `article_info` VALUES ('84', 'test', 'author', '0', '0', null, '2018-12-26 11:21:58', null, '1');
INSERT INTO `article_info` VALUES ('85', 'test', 'author', '0', '0', null, '2018-12-26 11:21:58', null, '1');
INSERT INTO `article_info` VALUES ('86', 'test', 'author', '0', '0', null, '2018-12-26 11:21:58', null, '1');
INSERT INTO `article_info` VALUES ('87', '0', '0', '0', '0', '2018-12-26 15:45:51', '2018-12-26 15:45:51', null, '0');
INSERT INTO `article_info` VALUES ('88', 'test article', 'monster', '0', '1', null, '2018-12-26 16:09:21', null, '1');
INSERT INTO `article_info` VALUES ('89', 'test article', 'monster', '1', '1', '2018-12-26 16:09:43', '2018-12-26 16:09:42', null, '1');
INSERT INTO `article_info` VALUES ('90', 'yeah', 'not', '1', '2', '2018-12-26 16:21:32', '2018-12-26 16:21:32', null, '1');
INSERT INTO `article_info` VALUES ('91', 'yeah', 'not', '1', '2', '2018-12-26 16:21:39', '2018-12-26 16:21:39', null, '1');
INSERT INTO `article_info` VALUES ('92', 'yeah', 'notanymore', '1', '2', '2018-12-26 16:30:03', '2018-12-26 16:30:04', null, '1');
INSERT INTO `article_info` VALUES ('93', 'empty', 'empty', '0', '1', null, '2018-12-26 16:37:31', null, '1');
INSERT INTO `article_info` VALUES ('94', 'empty', 'empty', '0', '1', null, '2018-12-26 16:37:50', null, '1');
INSERT INTO `article_info` VALUES ('95', 'ha', 'jeayeoi', '0', '1', null, '2018-12-26 16:52:13', null, '1');
